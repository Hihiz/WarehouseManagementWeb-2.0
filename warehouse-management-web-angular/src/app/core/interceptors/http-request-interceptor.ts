import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';

/**
 * Функция перехватыает HTTP запросы.
 */
export const httpRequestInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const accessToken = localStorage.getItem('utoken');

  console.log('accessToken', accessToken);

  if (accessToken) {
    req = req.clone({
      setHeaders: { Authorization: `Bearer ${accessToken}` },
    });
  }

  let responseNext = next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (req.url.includes('refresh-token')) {
        authService.clearStorage();
        router.navigate(['signin']);
        return throwError(() => error);
      }

      if (error.status !== 401) {
        return throwError(() => error);
      }

      return authService.refreshToken().pipe(
        switchMap((response: any) => {
          const newAccessToken = response.accessToken;
          const newRefreshToken = response.refreshToken;

          localStorage.setItem('utoken', newAccessToken);
          localStorage.setItem('urefresh-token', newRefreshToken);

          // Обновляем токены текущему пользователю.
          const currentUser = authService.userSignIn$.value;
          if (currentUser) {
            currentUser.accessToken = newAccessToken;
            currentUser.refreshToken = newRefreshToken;
            authService.userSignIn$.next(currentUser);
          }

          return next(
            req.clone({
              setHeaders: { Authorization: `Bearer ${newAccessToken}` },
            }),
          );
        }),

        catchError((err) => {
          authService.clearStorage();
          router.navigate(['signin']);
          return throwError(() => err);
        }),
      );
    }),
  );

  return responseNext;
};
