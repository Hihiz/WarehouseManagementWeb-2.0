import { ModifyResourceShipmentInput } from './modify-resource-shipment-input';

/**
 * Класс входной модели редактирования ресурса документа отгрузки.
 */
export class UpdateResourceShipmentInput {
  /**
   * Id документа отгрузки.
   */
  documentShipmentId: number = 0;

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
  documentShipmentClientId: number = 0;
  /**
   * Нужно ли подписать документ отгрузки.
   */
  isSetActiveStatus: boolean = false;

  /**
   * Список входящих ресурсов в документ отгрузки.
   */
  modifyResourceShipmentInputs: ModifyResourceShipmentInput[] = [];
}
