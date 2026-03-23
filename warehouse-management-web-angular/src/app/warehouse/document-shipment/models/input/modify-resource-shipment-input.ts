import { BalanceOutput } from '../../../balance/models/output/balance-output';

/**
 * Класс входной модели редактирования ресурса документа отгрузки.
 */
export class ModifyResourceShipmentInput {
  /**
   * Id ресурса отгрузки.
   */
  resourceShipmentId: number | null = null;

  /**
   * Id ресурса.
   */
  resourceId: number | null = null;

  /**
   * Id единицы измерения.
   */
  measureUnitId: number | null = null;

  /**
   * Количество ресурса.
   */
  resourceQuantity: number | null = null;

  /**
   * Свойство для UX.
   * Выбранный обьект баланса.
   */
  _selectedBalance: BalanceOutput | null = null;

  /**
   * Свойство для UX.
   * Выбранный id баланса.
   */
  _balanceId: string | null = null;

   /**
   * Cвойство для UX;
   * Начальное значение баланса.
   */
  _startBalanceQuantity: number = 0;
}
