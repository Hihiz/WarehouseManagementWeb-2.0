import { ModifyResourceReceiptInput } from './modify-resource-receipt-input';

/**
 * Класс входной модели редактирования ресурса поступления.
 */
export class UpdateResourceReceiptInput {
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
  date: Date = new Date();

  /**
   * Id клиента документа поступления.
   */
  documentReceiptClientId: number = 0;

  /**
   * Список входящих ресурсов поступлений.
   */
  modifyResourceReceiptInputs: ModifyResourceReceiptInput[] | null = null;
}
