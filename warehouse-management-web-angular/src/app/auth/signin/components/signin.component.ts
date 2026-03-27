import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserSignInOutput } from '../../models/output/user-sign-in-output';
import { BehaviorSubject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserSignInInput } from '../../models/input/user-sign-in-input';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-signin.component',
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css',
})
/**
 * Класс компонента аутентификации пользователей.
 */
export class SigninComponent implements OnInit {
  public userSignIn$ = new BehaviorSubject<UserSignInOutput | null>(null);

  userSignInInput: UserSignInInput = new UserSignInInput();
  errorMessage: string = '';

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
    this.userSignIn$ = this._authSerivce.userSignIn$;
  }

  /**
   * Функция аутентифицирует пользователя.
   */
  public onSendSignIn() {
    this._authSerivce.signIn(this.userSignInInput).subscribe({
      next: (_) => {
        console.log('Пользователь аутентифицирован: ', this.userSignIn$.value);

        this._router.navigate(['/clients']);
      },
      error: (error) => {
        console.log('Ошибка при прохождении аутентификации: ', error);

        this.errorMessage =
          error.error.message || 'Ошибка при прохождении аутентификации, повторите попытку !';

        this.cdr.detectChanges();
      },
    });
  }
}
