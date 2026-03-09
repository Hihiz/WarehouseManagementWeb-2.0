import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { UserSignInOutput } from '../models/output/user-sign-in-output';
import { UserSignUpOutput } from '../models/output/user-sign-up-output';
import { HttpClient } from '@angular/common/http';
import { UserSignUpInput } from '../models/input/user-sign-up-input';
import { environment } from '../../core/core-urls/environment';
import { UserSignInInput } from '../models/input/user-sign-in-input';
import { TokenInput } from '../models/input/token-input';

/**
 * Класс сервиса аутентификации пользователей.
 */
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public userSignUp$ = new BehaviorSubject<UserSignUpOutput>(new UserSignUpOutput());
  public userSignIn$ = new BehaviorSubject<UserSignInOutput>(new UserSignInOutput());

  /**
   * Конструктор.
   * @param _httpClient HttpClient.
   */
  constructor(private readonly _httpClient: HttpClient) {
    this.restoreUser();
  }

  /**
   * Функция регистрирует нового пользователя.
   * @param userSignUpInput Входная модель.
   * @returns Выходная модель.
   */
  public signUp(userSignUpInput: UserSignUpInput) {
    return this._httpClient
      .post(environment.apiUrl + '/api/account/signup', userSignUpInput)
      .pipe(tap((data: any) => this.userSignUp$.next(data)));
  }

  /**
   * Функция аутентифицирует пользователя.
   * @param userSignInInput Входная модель.
   * @returns Выходная модель.
   */
  public signIn(userSignInInput: UserSignInInput) {
    return this._httpClient.post(environment.apiUrl + '/api/account/signin', userSignInInput).pipe(
      tap((data: any) => {
        this.userSignIn$.next(data);

        this.saveStorage(data);
      }),
    );
  }

  /**
   * Функция обновляет токены пользователя (accessToken и refreshToken).
   */
  public refreshToken() {
    const accessToken = localStorage.getItem('utoken');
    const refreshToken = localStorage.getItem('urefresh-token');

    if (!accessToken || !refreshToken) {
      return throwError(() => new Error('Нет токенов для обновления.'));
    }

    const tokenInput: TokenInput = {
      accessToken: accessToken,
      refreshToken: refreshToken,
    };

    return this._httpClient.post(environment.apiUrl + '/api/account/refresh-token', tokenInput);
  }

  /**
   * Функция выходит из аккаунта пользователя.
   */
  public logout() {
    return this._httpClient.post(environment.apiUrl + '/api/account/logout', null).pipe(
      tap(() => {
        this.clearStorage();
        this.userSignIn$.next(new UserSignInOutput());
      }),
      catchError((error) => {
        this.clearStorage();
        this.userSignIn$.next(new UserSignInOutput());
        return throwError(() => error);
      }),
    );
  }

  /**
   * Функция полчает данные аутентифицированного пользователя из хранилища.
   */
  private restoreUser() {
    const token = localStorage.getItem('utoken');
    const refreshToken = localStorage.getItem('urefresh-token');
    const email = localStorage.getItem('uemail');

    if (token && refreshToken && email) {
      const user = new UserSignInOutput();

      user.accessToken = token;
      user.refreshToken = refreshToken;
      user.email = email;

      this.userSignIn$.next(user);
    }
  }

  /**
   * Фукнция очищает хранилище.
   */
  private clearStorage() {
    localStorage.removeItem('utoken');
    localStorage.removeItem('urefresh-token');
    localStorage.removeItem('uemail');
  }

  /**
   * Фукнция сохраняет данные в хранилище.
   */
  private saveStorage(response: UserSignInOutput) {
    localStorage.setItem('utoken', response.accessToken);
    localStorage.setItem('urefresh-token', response.refreshToken);
    localStorage.setItem('uemail', response.email);
  }
}
