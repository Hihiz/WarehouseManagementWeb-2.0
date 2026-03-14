import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс выходной модели единицы измерения.
 */
export class MeasureUnitOutput {
  /**
   * Id единицы измерения.
   */
  id: number = 0;

  /**
   * Наименование единицы измерения.
   */
  title: string = '';

  /**
   * Статус единицы измерения в значении перечисления.
   */
  measureUnitStatusEnum: DirectoryStatusEnum = 0;
}
