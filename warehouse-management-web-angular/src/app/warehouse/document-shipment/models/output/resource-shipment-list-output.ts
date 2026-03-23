import { DocumentStatusEnum } from '../../../enums/document-status-enum';
import { ResourceShipmentItemOutput } from './resource-shipment-item-output';

/**
 * Класс выходной модели ресурса документа отгрузки.
 */
export class ResourceShipmentListOutput {
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
   * Наименование клиента документа отгрузки.
   */
  documentShipmentClientName: string = '';

  /**
   * Статус документа отгрузки в значении перечисления.
   */
  documentStatusEnum: DocumentStatusEnum = 0;

  /**
   * Список входящих ресурсов в документ отгрузки.
   */
  items: ResourceShipmentItemOutput[] = [];
}
