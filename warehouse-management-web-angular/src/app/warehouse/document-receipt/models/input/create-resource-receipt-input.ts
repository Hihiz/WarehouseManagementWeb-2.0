import { IncludeResourceReceiptInput } from './include-resource-receipt-input';

/**
 *  Класс входной модели создания ресурса поступления.
 */
export class CreateResourceReceiptInput {
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
  clientId: number = 0;
  /**
   * Список входящих ресурсов поступления.
   */
  includeResourceReceiptInputs: IncludeResourceReceiptInput[] | null = null;
}