import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс входной модели обновления статуса клиента.
 */
export class ChangeStatusClientInput {
  /**
   *  Id клиента.
   */
  clientId: number = 0;

  /**
   * Статус клиента.
   */
  clientStatusEnum: DirectoryStatusEnum = 0;
}
