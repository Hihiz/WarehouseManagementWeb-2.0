/**
 * Класс входной модели добавления ресурсов в поступление.
 */
export class IncludeResourceReceiptInput {
  /**
   * Id ресурса.
   */
  resourceId: number = 0;

  /**
   * Id единицы измерения.
   */
  measureUnitId: number = 0;

  /**
   * Количество ресурса.
   */
  resourceQuantity: number = 0;
}