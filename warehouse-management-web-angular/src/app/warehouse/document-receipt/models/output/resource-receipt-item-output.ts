/**
 * Класс выходной модели входящих ресурсов поступления.
 */
export class ResourceReceiptItemOutput {
  /**
   * Id ресурса поступления.
   */
  resourceReceiptId: number = 0;

  /**
   * Id ресурса.
   */
  resourceId: number = 0;

  /**
   * Наименование ресурса.
   */
  resourceTitle: string = '';

  /**
   * Id единицы измерения.
   */
  measureUnitId: number = 0;

  /**
   * Наименование единицы измерения.
   */
  measureUnitTitle: string = '';

  /**
   * Количество ресурса.
   */
  resourceQuantity: number = 0;
}
