/**
 * Класс входной модели редактирования ресурса поступления.
 */
export class ModifyResourceReceiptInput {
  /**
   * Id ресурса поступления.
   */
  resourceReceiptId: number = 0;

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