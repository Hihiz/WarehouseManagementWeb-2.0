import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserSignUpOutput } from '../../models/output/user-sign-up-output';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { UserSignUpInput } from '../../models/input/user-sign-up-input';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup.component',
  imports: [FormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
})
/**
 * Класс компонента регистрации пользователей.
 */
export class SignupComponent implements OnInit {
  public userSignUp$ = new BehaviorSubject<UserSignUpOutput>(new UserSignUpOutput());

  userSignUpInput: UserSignUpInput = new UserSignUpInput();
  errorMessage: string = "";

  /**
   * Конструктор.
   * @param _authSerivce Сервис аутентификации пользователей.
   * @param _router Роутер.
   */
  constructor(
    private cdr: ChangeDetectorRef,
    private readonly _authSerivce: AuthService,
    private readonly _router: Router,
  ) {}

  ngOnInit() {
    this.userSignUp$ = this._authSerivce.userSignUp$;
  }

  /**
   * Функция регистрирует нового пользователя.
   */
  public onSendSignUp() {
    this._authSerivce.signUp(this.userSignUpInput).subscribe({
      next: (_) => {
        console.log('Пользователь зарегистрирован: ', this.userSignUp$.value);

        this._router.navigate(['/']);
      },
      error: (error) => {
        console.log('Ошибка при прохождении регистрации: ', error);

        this.errorMessage =
          error.error.message || 'Ошибка при прохождении регистрации, повторите попытку !';
     
        this.cdr.detectChanges();
      },
    });    
  }
}
