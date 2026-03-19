import { Injectable } from '@angular/core';

/**
 * Класс сервиса даты.
 */
@Injectable({
  providedIn: 'root',
})
export class DateService {
  /**
   * Функция возвращает текущюю дату.
   * @returns Текущая дата.
   */
  public getDateNow() {
    const now = new Date();
    const localNow = new Date(now.getTime() - now.getTimezoneOffset() * 60000)
      .toISOString()
      .slice(0, 16);

    return localNow as any;
  }

  /**
   * Функция форматирует дату.
   * @param date Дата.
   */
  public formatDate(date: Date) {
    return date.toString().substring(0, 16) as any;
  }
}
