import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс выходной модели клиентов.
 */
export class ClientOutput {
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

  /**
   * Статус клиента.
   */
  clientStatusEnum: DirectoryStatusEnum = 0;
}
