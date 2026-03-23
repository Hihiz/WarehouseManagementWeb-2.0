import { ChangeDetectorRef, Component, signal } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { UserSignInOutput } from './auth/models/output/user-sign-in-output';
import { AuthService } from './auth/services/auth.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLinkWithHref, AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('warehouse-management-web-angular');

  public readonly userSignIn$: BehaviorSubject<UserSignInOutput>;

  constructor(
    private readonly _authService: AuthService,
    private readonly router: Router,
    private readonly _cdr: ChangeDetectorRef,
  ) {
    this.userSignIn$ = _authService.userSignIn$;
  }

  /**
   * Функция выходит из аккаунта пользователя.
   */
  public onSendLogout() {
    this._authService.logout().subscribe({
      next: () => {
        console.log(`Пользователь: ${this.userSignIn$.value.email} успешно вышел из аккаунта.`);
        this.router.navigate(['/signin']);
        this._cdr.detectChanges();
      },
      error: (error) => {
        console.error('Ошибка при выходе из аккаунта: ', error);
        this.router.navigate(['/signin']);
        this._cdr.detectChanges();
      },
    });
  }
}
