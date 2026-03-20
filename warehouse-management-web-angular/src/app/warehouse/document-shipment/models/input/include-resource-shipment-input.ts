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
  resourceQuantity: number = 0;
}
