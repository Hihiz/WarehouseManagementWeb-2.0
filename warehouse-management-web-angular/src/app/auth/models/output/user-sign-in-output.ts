/**
 * Класс выходной модели прохождения аутентификации пользователя.
 */
export class UserSignInOutput {
  /**
   * Имя пользователя.
   */
  firstName: string = '';

  /**
   * Фамилия пользователя.
   */
  lastName: string = '';

  /**
   * Email пользователя.
   */
  email: string = '';

  /**
   * Токен доступа пользователя.
   */
  accessToken: string = '';

  /**
   * Токен обновления пользователя.
   */
  refreshToken: string = '';
}
