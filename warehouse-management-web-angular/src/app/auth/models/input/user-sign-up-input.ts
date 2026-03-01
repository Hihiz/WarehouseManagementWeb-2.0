/**
 * Класс входной модели регистрации пользователя.
 */
export class UserSignUpInput {
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
   * Пароль пользователя.
   */
  password: string = '';

  /**
   * Подтверждение пароля.
   */
  passwordConfirm: string = '';
}
