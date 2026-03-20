/**
 * Класс выходной модели входящих ресурсов отгрузки.
 */
export class ResourceShipmentItemOutput {
  /**
   * Id ресурса отгрузки.
   */
  resourceShipmentId: number = 0;

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
