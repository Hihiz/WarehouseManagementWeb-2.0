/**
 * Класс входной модели редактирования ресурса документа отгрузки.
 */
export class ModifyResourceShipmentInput {
  /**
   * Id ресурса отгрузки.
   */
  resourceShipmentId: number = 0;

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
  resourceQuantity: number = 0;
}
