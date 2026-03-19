/**
 * Класс входной модели добавления ресурсов в поступление.
 */
export class IncludeResourceReceiptInput {
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
  resourceQuantity: number = 0;
}