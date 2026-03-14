import { Component, OnInit } from '@angular/core';
import { MeasureUnitService } from '../../services/measure-unit.service';
import { BehaviorSubject } from 'rxjs';
import { MeasureUnitListByStatusOutput } from '../../models/output/measure-unit-list-by-status-output';
import { Router } from '@angular/router';
import { AsyncPipe, NgClass } from '@angular/common';

/**
 * Класс компонента единицы измерений.
 */
@Component({
  selector: 'app-measure-unit.component',
  imports: [NgClass, AsyncPipe],
  templateUrl: './measure-unit.component.html',
  styleUrl: './measure-unit.component.css',
})
export class MeasureUnitComponent implements OnInit {
  public measureUnits$ = new BehaviorSubject<MeasureUnitListByStatusOutput>(
    new MeasureUnitListByStatusOutput(),
  );

  /**
   * Конструктор.
   * @param _measureUnitService Сервис единиц измерений.
   * @param _router Роутер.
   */
  constructor(
    private readonly _measureUnitService: MeasureUnitService,
    private readonly _router: Router,
  ) {
    this.measureUnits$ = this._measureUnitService.measureUnits$;
  }

  isStatusMeasureUnitsActive: boolean = true;

  ngOnInit() {
    this.getMeasureUnits();
  }

  /**
   * Функция переключает списки единиц измерений по статусам.
   */
  public onSwitchMeasureUnits() {
    this.isStatusMeasureUnitsActive = !this.isStatusMeasureUnitsActive;
    console.log('Состояние переключателя: ', this.isStatusMeasureUnitsActive);
  }

  /**
   * Функция получает список единиц измерений.
   */
  private getMeasureUnits() {
    this._measureUnitService
      .getMeasureUnits()
      .subscribe((_) => console.log('Получен список единиц измерений: ', this.measureUnits$.value));
  }

  /**
   * Функция получает единицу измерения по Id.
   * @param measureUnitId Выбранный Id единицы измерения.
   */
  public onGetMeasureUnitById(measureUnitId: number) {
    this._router.navigate(['/detail-measure-unit'], {
      queryParams: {
        measureUnitId: measureUnitId,
      },
    });
  }

  /**
   * Функция переходит на страницу добавления единицы  измерений.
   */
  public onCreateMeasureUnit() {
    this._router.navigate(['/create-measure-unit']);
  }
}
