import { AsyncPipe, CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { MeasureUnitOutput } from '../../models/output/measure-unit-output';
import { MeasureUnitService } from '../../services/measure-unit.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UpdateMeasureUnitInput } from '../../models/input/update-measure-unit-input';
import { ChangeStatusMeasureUnitInput } from '../../models/input/change-status-measure-unit-input';
import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс компонента деталей единицы измерения.
 */
@Component({
  selector: 'app-detail-measure-unit.component',
  imports: [FormsModule, CommonModule, AsyncPipe],
  templateUrl: './detail-measure-unit.component.html',
  styleUrl: './detail-measure-unit.component.css',
})
export class DetailMeasureUnitComponent implements OnInit {
  public detailMeasureUnit$ = new BehaviorSubject<MeasureUnitOutput>(new MeasureUnitOutput());

  /**
   * Конструктор.
   * @param _measureUnitService Сервис единиц измерений.
   * @param _activatedRoute Роутер строки запроса.
   * @param _router Роутер.
   * @param cdr Обнаружение изменений.
   */
  constructor(
    private readonly _measureUnitService: MeasureUnitService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _router: Router,
    private cdr: ChangeDetectorRef,
  ) {
    this.detailMeasureUnit$ = this._measureUnitService.detailMeasureUnit$;
  }

  isLoader: boolean = true;
  serverNameError: string | null = null;
  updateMeasureUnitInput: UpdateMeasureUnitInput = new UpdateMeasureUnitInput();
  changeStatusMeasureUnitInput: ChangeStatusMeasureUnitInput = new ChangeStatusMeasureUnitInput();

  ngOnInit() {
    this.checkUrlParams();
  }

  /**
   * Функция берет значение из параметров URL.
   */
  private checkUrlParams() {
    this._activatedRoute.queryParams.subscribe((params) => {
      if (params['measureUnitId']) {
        this.updateMeasureUnitInput.id = params['measureUnitId'];
        this.getMeasureUnitById();
      }
    });
  }

  /**
   * Функция получает детали единицы измерения по Id.
   */
  private getMeasureUnitById() {
    this.isLoader = true;

    this._measureUnitService.getMeasureUnitById(this.updateMeasureUnitInput.id).subscribe((_) => {
      console.log('Детали единицы измерения ', this.detailMeasureUnit$.value);

      this.updateMeasureUnitInput.title = this.detailMeasureUnit$.value.title;

      this.isLoader = false;
    });
  }

  /**
   * Функция редактирует единицу измерения.
   */
  public onUpdateMeasureUnit() {
    this.serverNameError = null;

    this._measureUnitService.updateMeasureUnit(this.updateMeasureUnitInput).subscribe({
      next: (_) => {
        console.log('Единица измерения обновлена');

        this.updateMeasureUnitInput = new UpdateMeasureUnitInput();

        // Актуализация списка единиц измерений.
        this.onGetMeasureUnits();
      },
      error: (err) => {
        if (err.status === 400) {
          this.serverNameError =
            err.error.message ||
            'Единица измерения с таким наименованием уже существует в системе.';

          this.cdr.detectChanges();
        }
        console.error('Ошибка при редактировании единицы измерения:', err);
      },
    });
  }

  /**
   * Функция обновляет статус единицы измерения.
   */
  public async onChangeStatusMeasureUnit() {
    if (this.updateMeasureUnitInput.id !== 0) {
      this.changeStatusMeasureUnitInput.measureUnitId = this.updateMeasureUnitInput.id;
    }
    switch (this.detailMeasureUnit$.value.measureUnitStatusEnum.toString().toLowerCase()) {
      case 'active':
        this.changeStatusMeasureUnitInput.measureUnitStatusEnum = DirectoryStatusEnum.archived;
        break;

      case 'archived':
        this.changeStatusMeasureUnitInput.measureUnitStatusEnum = DirectoryStatusEnum.active;
        break;
    }

    this._measureUnitService
      .changeStatusMeasureUnit(this.changeStatusMeasureUnitInput)
      .subscribe((_) => {
        console.log('Статус единицы измерения изменен');

        this.onGetMeasureUnits();
      });
  }

  /**
   * Функция удаляет единицу измерения.
   */
  public onRemoveMeasureUnit() {
    this._measureUnitService.removeMeasureUnit(this.updateMeasureUnitInput.id).subscribe((_) => {
      console.log('Единица измерения удалена');

      this.onGetMeasureUnits();
    });
  }

  /**
   * Функция переходит на страницу списка единиц измерений.
   */
  public onGetMeasureUnits() {
    this._router.navigate(['/measure-units']);
  }
}
