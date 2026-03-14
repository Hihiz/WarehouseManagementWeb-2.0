import { Injectable } from '@angular/core';
import { MeasureUnitListByStatusOutput } from '../models/output/measure-unit-list-by-status-output';
import { BehaviorSubject, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../core/core-urls/environment';
import { MeasureUnitOutput } from '../models/output/measure-unit-output';
import { CreateMeasureUnitInput } from '../models/input/create-measure-unit-input';
import { UpdateMeasureUnitInput } from '../models/input/update-measure-unit-input';
import { ChangeStatusMeasureUnitInput } from '../models/input/change-status-measure-unit-input';

/**
 * Класс сервиса единиц измерений.
 */
@Injectable({
  providedIn: 'root',
})
export class MeasureUnitService {
  public measureUnits$ = new BehaviorSubject<MeasureUnitListByStatusOutput>(
    new MeasureUnitListByStatusOutput(),
  );
  public activeMeasureUnits$ = new BehaviorSubject<MeasureUnitOutput[]>([]);
  public detailMeasureUnit$ = new BehaviorSubject<MeasureUnitOutput>(new MeasureUnitOutput());

  /**
   * Конструктор.
   * @param _httpClient HttpClient.
   */
  constructor(private readonly _httpClient: HttpClient) {}

  /**
   * Функция получает список единиц измерений.
   * @returns Список единиц измерений.
   */
  public getMeasureUnits() {
    return this._httpClient
      .get<MeasureUnitListByStatusOutput>(
        environment.apiUrl + '/api/directory/measure-unit/measure-units',
      )
      .pipe(tap((data) => this.measureUnits$.next(data)));
  }

  /**
   * Функция получает список активных единиц измерений.
   * @returns Список активных единиц измерений.
   */
  public getActiveMeasureUnits() {
    return this._httpClient
      .get<MeasureUnitOutput[]>(environment.apiUrl + '/api/directory/measure-unit/active-measure-units')
      .pipe(tap((data) => this.activeMeasureUnits$.next(data)));
  }

  /**
   * Функция получает единицу измерения по Id.
   * @param measureUnitId Id единицы измерения.
   * @returns Данные единицы измерения.
   */
  public getMeasureUnitById(measureUnitId: number) {
    return this._httpClient
      .get<MeasureUnitOutput>(
        environment.apiUrl +
          `/api/directory/measure-unit/measure-unit?measureUnitId=${measureUnitId}`,
      )
      .pipe(tap((data) => this.detailMeasureUnit$.next(data)));
  }

  /**
   * Функция добавляет единицу измерения.
   * @param createMeasureUnitInput Входная модель.
   */
  public createMeasureUnit(createMeasureUnitInput: CreateMeasureUnitInput) {
    return this._httpClient.post<void>(
      environment.apiUrl + '/api/directory/measure-unit/measure-unit',
      createMeasureUnitInput,
    );
  }

  /**
   * Функция редактирует единицу измерения.
   * @param updateMeasureUnitInput Входная модель.
   */
  public updateMeasureUnit(updateMeasureUnitInput: UpdateMeasureUnitInput) {
    return this._httpClient.put<void>(
      environment.apiUrl + '/api/directory/measure-unit/measure-unit',
      updateMeasureUnitInput,
    );
  }

  /**
   * Функция обновляет статус единице измерения.
   * @param changeStatusMeasureUnitInput Входная модель.
   */
  public changeStatusMeasureUnit(changeStatusMeasureUnitInput: ChangeStatusMeasureUnitInput) {
    return this._httpClient.patch<void>(
      environment.apiUrl + '/api/directory/measure-unit/measure-unit',
      changeStatusMeasureUnitInput,
    );
  }

  /**
   * Функция удаляет единицу измерения.
   * @param measureUnitId Id единицы измерения.
   */
  public removeMeasureUnit(measureUnitId: number) {
    return this._httpClient.delete<void>(
      environment.apiUrl + '/api/directory/measure-unit/measure-unit',
      {
        body: JSON.stringify(measureUnitId),
        headers: { 'Content-Type': 'application/json' },
      },
    );
  }
}
