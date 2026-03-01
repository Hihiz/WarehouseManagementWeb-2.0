import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

/**
 * Функция проверяет авторизацию пользователя.
 * @returns Признак авторизации.
 */
export const authGuard: CanActivateFn = () => {
  const router = inject(Router);

  // Проверяем наличие токена в localStorage.
  const token = localStorage.getItem('utoken');

  if (token) {
    return true; // Разрешаем доступ.
  }

  // Перенаправляем на страницу аутентификации.
  return router.parseUrl('/signin');
};
