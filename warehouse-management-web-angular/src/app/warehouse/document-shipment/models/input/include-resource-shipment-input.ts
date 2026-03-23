import { BalanceOutput } from '../../../balance/models/output/balance-output';

/**
 * Класс входной модели добавления ресурсов в документ отгрузки.
 */
export class IncludeResourceShipmentInput {
  /**
   * Id ресурса.
   */
  resourceId: number | null = null;

  /**
   * Id единицы измерения.
   */
  measureUnitId?: number | null = null;

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
}
