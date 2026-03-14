import { MeasureUnitOutput } from './measure-unit-output';

/**
 * Класс выходной модели единиц измерений разделенных по статусам.
 */
export class MeasureUnitListByStatusOutput {
  /**
   * Список активных единиц измерения.
   */
  activeMeasureUnits: MeasureUnitOutput[] = [];

  /**
   * Список единиц измерения находящихся в архиве.
   */
  archivedMeasureUnits: MeasureUnitOutput[] = [];
}
