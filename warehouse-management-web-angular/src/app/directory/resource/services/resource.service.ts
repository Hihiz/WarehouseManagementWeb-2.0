import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { ResourceListByStatusOutput } from '../models/output/resource-list-by-status-output';
import { ResourceOutput } from '../models/output/resource-output';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../core/core-urls/environment';
import { CreateResourceInput } from '../models/input/create-resource-input';
import { UpdateResourceInput } from '../models/input/update-resource-input';
import { ChangeStatusResourceInput } from '../models/input/change-status-resource-input';

/**
 * Класс сервиса ресурсов.
 */
@Injectable({
  providedIn: 'root',
})
export class ResourceService {
  public resources$ = new BehaviorSubject<ResourceListByStatusOutput>(
    new ResourceListByStatusOutput(),
  );
  public activeResources$ = new BehaviorSubject<ResourceOutput[]>([]);
  public detailResource$ = new BehaviorSubject<ResourceOutput>(new ResourceOutput());

  /**
   * Конструктор.
   * @param _httpClient HttpClient.
   */
  constructor(private readonly _httpClient: HttpClient) {}

  /**
   * Функция получает список ресурсов.
   * @returns Список ресурсов.
   */
  public getResources() {
    return this._httpClient
      .get<ResourceListByStatusOutput>(environment.apiUrl + '/api/directory/resource/resources')
      .pipe(tap((data) => this.resources$.next(data)));
  }

  /**
   * Функция получает список активных ресурсов.
   * @returns Список активных ресурсов.
   */
  public getActiveResources() {
    return this._httpClient
      .get<ResourceOutput[]>(environment.apiUrl + '/api/directory/resource/active-resources')
      .pipe(tap((data) => this.activeResources$.next(data)));
  }

  /**
   * Функция получает ресурс по Id.
   * @param resourceId Id ресурса.
   * @returns Данные ресурса.
   */
  public getResourceById(resourceId: number) {
    return this._httpClient
      .get<ResourceOutput>(
        environment.apiUrl + `/api/directory/resource/resource?resourceId=${resourceId}`,
      )
      .pipe(tap((data) => this.detailResource$.next(data)));
  }

  /**
   * Функция добавляет ресурс.
   * @param createResourceInput Входная модель.
   */
  public CreateResourceAsync(createResourceInput: CreateResourceInput) {
    return this._httpClient.post<void>(
      environment.apiUrl + '/api/directory/resource/resource',
      createResourceInput,
    );
  }

  /**
   * Функция редактирует ресурс.
   * @param updateResourceInput Входная модель.
   */
  public updateResource(updateResourceInput: UpdateResourceInput) {
    return this._httpClient.put<void>(
      environment.apiUrl + '/api/directory/resource/resource',
      updateResourceInput,
    );
  }

  /**
   * Функция обновляет статус ресурсу.
   * @param changeStatusResourceInput Входная модель.
   */
  public changeStatusResource(changeStatusResourceInput: ChangeStatusResourceInput) {
    return this._httpClient.patch<void>(
      environment.apiUrl + '/api/directory/resource/change-status-resource',
      changeStatusResourceInput,
    );
  }

  /**
   * Функция удаляет ресурс.
   * @param resourceId Id ресурса.
   */
  public removeResource(resourceId: number) {
    return this._httpClient.delete<void>(environment.apiUrl + '/api/directory/resource/resource', {
      body: JSON.stringify(resourceId),
      headers: { 'Content-Type': 'application/json' },
    });
  }
}
