import { DocumentStatusEnum } from '../../../enums/document-status-enum';

/**
 * Класс входной модели обновления статуса документа отгрузки.
 */
export class ChangeStatusDocumentShipmentInput {
  /**
   * Id документа отгрузки.
   */
  documentShipmentId: number = 0;

  /**
   * Статус документа отгрузки.
   */
  documentShipmentStatusEnum: DocumentStatusEnum = 1;
}
