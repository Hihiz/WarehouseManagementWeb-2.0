import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BehaviorSubject, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';

// let isRefreshing = false;
// const refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(
//   null,
// );

/**
 * Функция перехватыает HTTP запросы.
 */
// export const httpRequestInterceptor: HttpInterceptorFn = (req, next) => {
//   const authService = inject(AuthService);
//   const router = inject(Router);

//   const accessToken = localStorage.getItem('utoken');

//   console.log('accessToken', accessToken);

//   if (req.url.includes('refresh-token')) {
//     return next(req);
//   }

//   if (accessToken) {
//     req = req.clone({
//       setHeaders: { Authorization: `Bearer ${accessToken}` },
//     });
//   }

//   let responseNext = next(req).pipe(
//     catchError((error: HttpErrorResponse) => {
//       if (error.status !== 401) {
//         return throwError(() => error);
//       }

//       if (isRefreshing) {
//         // Уже идёт обновление, ждём новый токен.
//         return refreshTokenSubject.pipe(
//           take(1),
//           switchMap((token) => {
//             if (!token) {
//               return throwError(() => error);
//             }
//             return next(
//               req.clone({
//                 setHeaders: { Authorization: `Bearer ${token}` },
//               }),
//             );
//           }),
//         );
//       }

//       isRefreshing = true;
//       refreshTokenSubject.next(null);

//       return authService.refreshToken().pipe(
//         switchMap((response: any) => {
//           const newAccessToken = response.accessToken;
//           const newRefreshToken = response.refreshToken;

//           localStorage.setItem('utoken', newAccessToken);
//           localStorage.setItem('urefresh-token', newRefreshToken);

//           // Обновляем токены текущему пользователю.
//           const currentUser = authService.userSignIn$.value;
//           if (currentUser) {
//             currentUser.accessToken = newAccessToken;
//             currentUser.refreshToken = newRefreshToken;
//             authService.userSignIn$.next(currentUser);
//           }

//           isRefreshing = false;
//           refreshTokenSubject.next(newAccessToken);

//           return next(
//             req.clone({
//               setHeaders: { Authorization: `Bearer ${newAccessToken}` },
//             }),
//           );
//         }),

//         catchError((err) => {
//           isRefreshing = false;
//           refreshTokenSubject.error(err);

//           authService.clearStorage();
//           router.navigate(['/signin']);
//           return throwError(() => err);
//         }),
//       );
//     }),
//   );

//   return responseNext;
// };

export const httpRequestInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const accessToken = localStorage.getItem('utoken');

   if (accessToken) {
    req = req.clone({
      setHeaders: { Authorization: `Bearer ${accessToken}` },
    });
  }

// Обрабатываем ответы и ошибки
  return next(req).pipe(
    catchError(error => {
      if (error.status === 401) {
       authService.clearStorage();
        // Редирект на страницу входа.
        router.navigate(['/signin']);
      }

      return throwError(() => error);
    })
  );
};