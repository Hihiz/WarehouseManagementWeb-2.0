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
  resourceId: number | null = null;

  /**
   * Id единицы измерения.
   */
  measureUnitId: number | null = null;

  /**
   * Количество ресурса.
   */
  resourceQuantity: number | null = null;
}