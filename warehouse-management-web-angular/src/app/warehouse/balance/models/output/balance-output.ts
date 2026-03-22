export class BalanceOutput {
  /**
   * Id баланса.
   */
  id: number = 0;

  /**
   * Id ресурса.
   */
  resourceId: number = 0;

  /**
   * Наименование ресурса.
   */
  resourceTitle: string  = '';

  /**
   * Id единицы измерения.
   */
  measureUnitId: number = 0;

  /**
   * Наименование единицы измерения.
   */
  measureUnitTitle: string  = '';

  /**
   * Остаток ресурса.
   */
  availableQuantity: number = 0;
}
