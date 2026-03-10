/**
 * Класс входной модели редактирования клиента.
 */
export class UpdateClientInput {
  /**
   * Id клиента.
   */
  id: number = 0;

  /**
   * Наименование клиента.
   */
  name: string = '';

  /**
   * Адрес клиента.
   */
  address: string = '';
}
