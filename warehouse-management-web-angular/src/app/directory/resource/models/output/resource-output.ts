import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс выходной модели ресурса.
 */
export class ResourceOutput {
  /**
   * Id ресурса.
   */
  id: number = 0;

  /**
   * Наименование ресурса.
   */
  title: string = '';

  /**
   * Статус ресурса в значении перечисления.
   */
  resourceStatusEnum: DirectoryStatusEnum = 0;
}
