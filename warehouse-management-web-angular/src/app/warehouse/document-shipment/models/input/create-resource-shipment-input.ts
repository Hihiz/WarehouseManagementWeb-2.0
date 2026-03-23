import { IncludeResourceShipmentInput } from './include-resource-shipment-input';

/**
 * Класс входной модели создания ресурса отгрузки.
 */
export class CreateResourceShipmentInput {
  /**
   * Номер документа отгрузки.
   */
  documentShipmentNumberCode: string = '';
  /**
   * Дата документа отгрузки.
   */
  documentShipmentDate: Date = new Date();
  /**
   * Id клиента документа отгрузки.
   */
  documentShipmentClientId: number | null = null;

  /**
   * Нужно ли подписать документ отгрузки.
   */
  isSetActiveStatus: boolean = false;

  /**
   * Список входящих ресурсов в документ отгрузки.
   */
  includeResourceShipmentInputs: IncludeResourceShipmentInput[] = [];
}
