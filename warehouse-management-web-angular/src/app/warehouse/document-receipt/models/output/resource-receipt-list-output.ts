import { ResourceReceiptItemOutput } from './resource-receipt-item-output';

/**
 * Класс выходной модели ресурсов поступлений.
 */
export class ResourceReceiptListOutput {
  /**
   * Id документа поступления.
   */
  documentReceiptId: number = 0;

  /**
   * Номер документа поступления.
   */
  documentReceiptNumberCode: string = '';

  /**
   * Дата документа поступления.
   */
  documentReceiptDate: Date = new Date();

  /**
   * Id клиента документа поступления.
   */
  documentReceiptClientId: number = 0;

  /**
   * Наименование клиента документа поступения.
   */
  documentReceiptClientName: string = '';

  /**
   * Список входящих ресурсов.
   */
  items: ResourceReceiptItemOutput[]  = [];
}
