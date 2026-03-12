import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс входной модели обновления статуса ресурса.
 */
export class ChangeStatusResourceInput {
  /**
   *  Id ресурса.
   */
  resourceId: number = 0;

  /**
   * Статус ресурса.
   */
  resourceStatusEnum: DirectoryStatusEnum = 0;
}
