import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

/**
 * Функция проверяет аутентификацию пользователя.
 * @returns Признак аутентификации.
 */
export const noAuthGuard: CanActivateFn = () => {
  const router = inject(Router);

  // Проверяем наличие токена в localStorage.
  const token = localStorage.getItem('utoken');

  if (token) {
    // Если пользователь аутентифицирован, не пускаем на signin/signup.
    return router.navigate(['/']);
  }

  // Перенаправляем на страницу signin/signup.
  return true;
};
