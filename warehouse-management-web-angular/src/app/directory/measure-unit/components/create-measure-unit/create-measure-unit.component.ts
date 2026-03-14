import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateMeasureUnitInput } from '../../models/input/create-measure-unit-input';
import { MeasureUnitService } from '../../services/measure-unit.service';
import { Router } from '@angular/router';

/**
 * Класс компонента добавления единицы измерения.
 */
@Component({
  selector: 'app-create-measure-unit.component',
  imports: [FormsModule, CommonModule],
  templateUrl: './create-measure-unit.component.html',
  styleUrl: './create-measure-unit.component.css',
})
export class CreateMeasureUnitComponent {
  /**
   * Конструктор.
   * @param _measureUnitService Сервис единиц измерений.
   * @param _router Роутер.
   * @param cdr Обнаружение изменений.
   */
  constructor(
    private readonly _measureUnitService: MeasureUnitService,
    private readonly _router: Router,
    private readonly cdr: ChangeDetectorRef,
  ) {}

  serverNameError: string | null = null;
  createMeasureUnitInput: CreateMeasureUnitInput = new CreateMeasureUnitInput();

  /**
   * Функция добавляет единицу измерения.
   */
  public onCreateMeasureUnit() {
    this._measureUnitService.createMeasureUnit(this.createMeasureUnitInput).subscribe({
      next: (_) => {
        console.log('Единица измерения добавлена');
        this.createMeasureUnitInput = new CreateMeasureUnitInput();

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

        console.error('Ошибка при создании единицы измерения: ', err);
      },
    });
  }

  /**
   * Функция переходит к списку единиц измерений.
   */
  public onGetMeasureUnits() {
    this._router.navigate(['/measure-units']);
  }
}
