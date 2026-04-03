import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { ClientListByStatusOutput } from '../models/output/client-list-by-status-output';
import { ClientOutput } from '../models/output/client-output';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../core/core-urls/environment';
import { CreateClientInput } from '../models/input/create-client-input';
import { UpdateClientInput } from '../models/input/update-client-input';
import { ChangeStatusClientInput } from '../models/input/change-status-client-input';

/**
 * Класс сервиса клиентов.
 */
@Injectable({
  providedIn: 'root',
})
export class ClientService {
  public clients$ = new BehaviorSubject<ClientListByStatusOutput>(new ClientListByStatusOutput());
  public activeClients$ = new BehaviorSubject<ClientOutput[]>([]);
  public detailClient$ = new BehaviorSubject<ClientOutput>(new ClientOutput());

  /**
   * Конструктор.
   * @param _httpClient HttpClient.
   */
  constructor(private readonly _httpClient: HttpClient) {}

  /**
   * Функция получает список клиентов.
   * @returns Список клиентов.
   */
  public getClients() {
    return this._httpClient
      .get<ClientListByStatusOutput>(environment.apiUrl + '/api/directory/client/clients')
      .pipe(tap((data) => this.clients$.next(data)));
  }

  /**
   * Функция получает список активных клиентов.
   * @returns Список активных клиентов.
   */
  public getActiveClients(clientId: number | null = null) {
    let params = new HttpParams();

    if (clientId) {
      params = params.append('clientId', clientId);
    }

    console.log('Params проверка: ', params);
    return this._httpClient
      .get<ClientOutput[]>(environment.apiUrl + `/api/directory/client/active-clients`, { params })
      .pipe(tap((data) => this.activeClients$.next(data)));
  }

  /**
   * Функция получает клиента по Id.
   * @param clientId Id клиента.
   * @returns Данные клиента.
   */
  public getClientById(clientId: number) {
    return this._httpClient
      .get<ClientOutput>(environment.apiUrl + `/api/directory/client/client?clientId=${clientId}`)
      .pipe(tap((data) => this.detailClient$.next(data)));
  }

  /**
   * Функция добавляет клиента.
   * @param createClientInput Входная модель.
   */
  public createClient(createClientInput: CreateClientInput) {
    return this._httpClient.post<void>(
      environment.apiUrl + '/api/directory/client/client',
      createClientInput,
    );
  }

  /**
   * Функция редактирует клиента.
   * @param updateClientInput Входная модель.
   */
  public updateClient(updateClientInput: UpdateClientInput) {
    return this._httpClient.put<void>(
      environment.apiUrl + '/api/directory/client/client',
      updateClientInput,
    );
  }

  /**
   * Функция обновляет статус клиенту.
   * @param changeStatusClientInput Входная модель.
   */
  public changeStatusClient(changeStatusClientInput: ChangeStatusClientInput) {
    return this._httpClient.patch<void>(
      environment.apiUrl + '/api/directory/client/change-status-client',
      changeStatusClientInput,
    );
  }

  /**
   * Функция удаляет клиента.
   * @param clientId Id клиента.
   */
  public removeClient(clientId: number) {
    return this._httpClient.delete<void>(environment.apiUrl + '/api/directory/client/client', {
      body: JSON.stringify(clientId),
      headers: { 'Content-Type': 'application/json' },
    });
  }
}
