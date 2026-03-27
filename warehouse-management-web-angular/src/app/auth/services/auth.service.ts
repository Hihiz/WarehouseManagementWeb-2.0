import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, debounceTime, filter, Subject, tap, throwError } from 'rxjs';
import { UserSignInOutput } from '../models/output/user-sign-in-output';
import { UserSignUpOutput } from '../models/output/user-sign-up-output';
import { HttpClient } from '@angular/common/http';
import { UserSignUpInput } from '../models/input/user-sign-up-input';
import { environment } from '../../core/core-urls/environment';
import { UserSignInInput } from '../models/input/user-sign-in-input';
import { TokenInput } from '../models/input/token-input';
import { Router } from '@angular/router';

/**
 * Класс сервиса аутентификации пользователей.
 */
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public userSignUp$ = new BehaviorSubject<UserSignUpOutput>(new UserSignUpOutput());
  public userSignIn$ = new BehaviorSubject<UserSignInOutput | null>(null);

  private storageEvent$ = new Subject<StorageEvent>();

  /**
   * Конструктор.
   * @param _httpClient HttpClient.
   */
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _router: Router,
  ) {
    this.restoreUser();
    this.initStorageEventListener();
  }

  /**
   * Функция подписывается на событие "storage".
   * Для синхронизации состояния пользователя между вкладками браузера.
   */
  private initStorageEventListener() {
    window.addEventListener('storage', (event) => {
      this.storageEvent$.next(event);
    });

    this.storageEvent$
      .pipe(
        filter((e) => e.key === 'utoken'),
        debounceTime(50),
      )
      .subscribe((e) => {
        if (e.newValue === null) {
          this.userSignIn$.next(null);
          this._router.navigate(['/signin']);
        } else if (e.newValue && e.newValue !== e.oldValue) {
          this.restoreUser();
          this._router.navigate(['/']);
        }
      });
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
      }),
      catchError((error) => {
        this.clearStorage();
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
    } else {
      this.userSignIn$.next(null);
    }
  }

  /**
   * Фукнция очищает хранилище.
   */
  public clearStorage() {
    localStorage.removeItem('utoken');
    localStorage.removeItem('urefresh-token');
    localStorage.removeItem('uemail');

    this.userSignIn$.next(null);
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
