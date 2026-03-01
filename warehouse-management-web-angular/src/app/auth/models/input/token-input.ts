/**
 * Класс входной модели токенов пользователя.
 */
export class TokenInput {
  /**
   * Токен доступа пользователя.
   */
  accessToken: string = '';

  /**
   * Токен обновления пользователя.
   */
  refreshToken: string = '';
}
