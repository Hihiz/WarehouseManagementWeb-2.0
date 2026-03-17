import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { ResourceReceiptListOutput } from '../models/output/resource-receipt-list-output';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../core/core-urls/environment';
import { CreateResourceReceiptInput } from '../models/input/create-resource-receipt-input';
import { UpdateResourceReceiptInput } from '../models/input/update-resource-receipt-input';

/**
 * Класс сервиса документов поступлений.
 */
@Injectable({
  providedIn: 'root',
})
export class DocumentReceiptSerivce {
  public documentReceipts$ = new BehaviorSubject<ResourceReceiptListOutput[]>([]);
  public detailDocumentReceipt$ = new BehaviorSubject<ResourceReceiptListOutput>(new ResourceReceiptListOutput);

  /**
   * Конструктор.
   * @param _httpClient HttpClient 
   */
  constructor(private readonly _httpClient: HttpClient){}

 /**
   * Функция получает список ресурсов поступления.
   * @returns Список ресурсов поступления.
   */
  public getResourceReceipts() {
    return this._httpClient
      .get<ResourceReceiptListOutput[]>(
        environment.apiUrl + '/api/warehouse/document-receipt/document-receipts')
        .pipe(tap((data) => this.documentReceipts$.next(data)));
  }

  /**
   * Функция получает ресурс поступления по Id документа поступления.
   * @param documentReceiptId Id документа поступления.
   * @returns Данные ресурса поступления.
   */
  public getResourceReceiptByDocumentReceiptId(documentReceiptId: number) {
    return this._httpClient
      .get<ResourceReceiptListOutput>(
       environment.apiUrl + `/api/warehouse/document-receipt/document-receipt?documentReceiptId=${documentReceiptId}`)
      .pipe(tap((data) => this.detailDocumentReceipt$.next(data)));
  }

  /**
   * Функция создает документ поступления и добавляет ресурсы поступления.
   * @param input Входная модель.
   */
  public createResourceReceipt(input: CreateResourceReceiptInput) {
    return this._httpClient.post<void>(
        environment.apiUrl + '/api/warehouse/document-receipt/document-receipt', input);
  }

  /**
   * Функция редактирует ресурс поступления.
   * @param input Входная модель.
   */
  public updateResourceReceipt( input: UpdateResourceReceiptInput) {
    return this._httpClient.put<void>(
        environment.apiUrl + '/api/warehouse/document-receipt/document-receipt', input);
  }

  /**
   * Функция удаляет документ поступления.
   * @param documentReceiptId Id документа поступления.
   */
  public removeDocumentReceipt(documentReceiptId: number) {
    return this._httpClient.delete<void>(
       environment.apiUrl + '/api/warehouse/document-receipt/document-receipt',
      {
        body: JSON.stringify(documentReceiptId),
        headers: { 'Content-Type': 'application/json' },
      }
    );
  }
}
