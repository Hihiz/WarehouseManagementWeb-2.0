import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс входной модели обновления статуса единицы измерения.
 */
export class ChangeStatusMeasureUnitInput {
  /**
   * Id единицы измерения.
   */
  measureUnitId: number = 0;

  /**
   * Статус единицы измерения.
   */
  measureUnitStatusEnum: DirectoryStatusEnum = 1;
}
