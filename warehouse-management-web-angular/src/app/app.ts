import { Component, Signal, signal } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, Router } from '@angular/router';
import { UserSignInOutput } from './auth/models/output/user-sign-in-output';
import { AuthService } from './auth/services/auth.service';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLinkWithHref],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('warehouse-management-web-angular');

  public user: Signal<UserSignInOutput | null | undefined>;

  constructor(
    private readonly _authService: AuthService,
    private readonly router: Router,
  ) {
    this.user = toSignal(this._authService.userSignIn$);
  }

  userEmail: string = '';

  /**
   * Функция выходит из аккаунта пользователя.
   */
  public onSendLogout() {
    this.userEmail = this.user()?.email!;
    
    this._authService.logout().subscribe({
      next: () => {
        console.log(`Пользователь: ${this.userEmail} успешно вышел из аккаунта.`);
        this.router.navigate(['/signin']);
      },
      error: (error) => {
        console.error('Ошибка при выходе из аккаунта: ', error);
        this.router.navigate(['/signin']);
      },
    });
  }
}
