import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { BalanceOutput } from '../models/output/balance-output';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../core/core-urls/environment';

/**
 * Класс сервис балансов.
 */
@Injectable({
  providedIn: 'root',
})
export class BalanceService {
  public balances$ = new BehaviorSubject<BalanceOutput[]>([]);

  /**
   * Конструктор.
   * @param _httpClient HttpClient
   */
  constructor(private readonly _httpClient: HttpClient) {}

  /**
   * Функция получает список доступных ресурсов баланса.
   * @returns Список доступных ресурсов баланса.
   */
  public getAvailableBalances() {
    return this._httpClient
      .get<BalanceOutput[]>(environment.apiUrl + '/api/warehouse/balance/balances')
      .pipe(tap((data) => this.balances$.next(data)));
  }
}
