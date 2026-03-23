import { Injectable } from '@angular/core';
import { ResourceShipmentListOutput } from '../models/output/resource-shipment-list-output';
import { BehaviorSubject, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../core/core-urls/environment';
import { CreateResourceShipmentInput } from '../models/input/create-resource-shipment-input';
import { UpdateResourceShipmentInput } from '../models/input/update-resource-shipment-input';

/**
 * Класс сервиса документов отгрузок.
 */
@Injectable({
  providedIn: 'root',
})
export class DocumentShipmentService {
  public documentShipments$ = new BehaviorSubject<ResourceShipmentListOutput[]>([]);
  public detailDocumentShipment$ = new BehaviorSubject<ResourceShipmentListOutput>(
    new ResourceShipmentListOutput(),
  );

  /**
   * Конструктор.
   * @param _httpClient HttpClient
   */
  constructor(private readonly _httpClient: HttpClient) {}

  /**
   * Функция получает список ресурсов отгрузки.
   * @returns Список ресурсов поступления.
   */
  public getResourceShipments() {
    return this._httpClient
      .get<
        ResourceShipmentListOutput[]
      >(environment.apiUrl + '/api/warehouse/document-shipment/document-shipments')
      .pipe(tap((data) => this.documentShipments$.next(data)));
  }

  /**
   * Функция получает ресурс отгрузки по Id документа отгрузки.
   * @param documentReceiptId Id документа отгрузки.
   * @returns Данные ресурса отгрузки.
   */
  public getResourceShipmentByDocumentShipmentId(documentShipmentId: number) {
    return this._httpClient
      .get<ResourceShipmentListOutput>(
        environment.apiUrl +
          `/api/warehouse/document-shipment/document-shipment?documentShipmentId=${documentShipmentId}`,
      )
      .pipe(tap((data) => this.detailDocumentShipment$.next(data)));
  }

  /**
   * Функция создает документ отгрузки и добавляет ресурсы отгрузки.
   * @param input Входная модель.
   */
  public createResourceShipment(input: CreateResourceShipmentInput) {
    return this._httpClient.post<void>(
      environment.apiUrl + '/api/warehouse/document-shipment/document-shipment',
      input,
    );
  }

  /**
   * Функция редактирует ресурсы отгрузки.
   * @param input Входная модель.
   */
  public updateResourceShipment(input: UpdateResourceShipmentInput) {
    return this._httpClient.put<void>(
      environment.apiUrl + '/api/warehouse/document-shipment/document-shipment',
      input,
    );
  }

  /**
   * Функция удаляет документ отгрузки.
   * @param documentReceiptId Id документа отгрузки.
   */
  public removeDocumentShipment(documentShipmentId: number) {
    return this._httpClient.delete<void>(
      environment.apiUrl + '/api/warehouse/document-shipment/document-shipment',
      {
        body: JSON.stringify(documentShipmentId),
        headers: { 'Content-Type': 'application/json' },
      },
    );
  }
}
