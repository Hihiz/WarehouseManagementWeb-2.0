import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateResourceReceiptInput } from '../../models/input/create-resource-receipt-input';
import { DocumentReceiptSerivce } from '../../services/document-receipt.serivce';
import { Router } from '@angular/router';
import { MeasureUnitOutput } from '../../../../directory/measure-unit/models/output/measure-unit-output';
import { ClientOutput } from '../../../../directory/client/models/output/client-output';
import { ResourceOutput } from '../../../../directory/resource/models/output/resource-output';
import { MeasureUnitService } from '../../../../directory/measure-unit/services/measure-unit.service';
import { ClientService } from '../../../../directory/client/services/client.service';
import { ResourceService } from '../../../../directory/resource/services/resource.service';
import { BehaviorSubject, forkJoin, tap } from 'rxjs';
import { AsyncPipe, NgClass } from '@angular/common';
import { DateService } from '../../../../helpers/date.service';

/**
 * Класс компонента создания документа поступления.
 */
@Component({
  selector: 'app-create-document-receipt.component',
  imports: [FormsModule, NgClass, AsyncPipe],
  templateUrl: './create-document-receipt.component.html',
  styleUrl: './create-document-receipt.component.css',
})
export class CreateDocumentReceiptComponent implements OnInit {
  public activeMeasureUnits$ = new BehaviorSubject<MeasureUnitOutput[]>([]);
  public activeClients$ = new BehaviorSubject<ClientOutput[]>([]);
  public activeResources$ = new BehaviorSubject<ResourceOutput[]>([]);

  /**
   * Конструктор.
   * @param _documentReceiptService Сервис документов поступлений.
   */
  constructor(
    private readonly _measureUnitSerivce: MeasureUnitService,
    private readonly _clientService: ClientService,
    private readonly _resourceService: ResourceService,
    private readonly _documentReceiptService: DocumentReceiptSerivce,
    private readonly _router: Router,
    private _cdr: ChangeDetectorRef,
    private readonly _dateService: DateService,
  ) {
    this.activeMeasureUnits$ = this._measureUnitSerivce.activeMeasureUnits$;
    this.activeClients$ = this._clientService.activeClients$;
    this.activeResources$ = this._resourceService.activeResources$;
  }

  tableResourcesError: string | null = null;
  isLoader: boolean = true;
  serverNameError: string | null = null;
  createResourceReceiptInput: CreateResourceReceiptInput = new CreateResourceReceiptInput();

  ngOnInit() {
    this.createResourceReceiptInput.date = this._dateService.getDateNow();

    forkJoin([
      this.getActiveClients(),
      this.getActiveMeasureUnits(),
      this.getActiveResources(),
    ]).subscribe({
      next: () => {
        this.isLoader = false;
        this._cdr.detectChanges();
      },
      error: (err) => {
        console.log('Ошибка при загрузке данных: ', err);
        this.isLoader = false;
           this._cdr.detectChanges();
      },
    });
  }

  /**
   * Фукнция получает список активных ресурсов для заполнения выпадающего списка.
   */
  private getActiveResources() {
    return this._resourceService.getActiveResources().pipe(
      tap(() => {
        console.log('Получен список активных ресурсов: ', this.activeResources$.value);
      }),
    );
  }

  /**
   * Фукнция получает список активных единиц измерений для заполнения выпадающего списка.
   */
  private getActiveMeasureUnits() {
    return this._measureUnitSerivce.getActiveMeasureUnits().pipe(
      tap(() => {
        console.log('Получен список активных единиц измерений: ', this.activeMeasureUnits$.value);
      }),
    );
  }

  /**
   * Фукнция получает список активных клиентов для заполнения выпадающего списка.
   */
  private getActiveClients() {
    return this._clientService.getActiveClients().pipe(
      tap(() => {
        console.log('Получен список активных клиентов: ', this.activeClients$.value);
      }),
    );
  }

  /**
   * Функция добавляет пустую строку ресурса в список.
   */
  public onIncludeResourceReceipt() {
    if (!this.createResourceReceiptInput.includeResourceReceiptInputs) {
      this.createResourceReceiptInput.includeResourceReceiptInputs = [];
    }

    // Добавляем новый обьект с начальными значениями.
    this.createResourceReceiptInput.includeResourceReceiptInputs.push({
      resourceId: null,
      measureUnitId: null,
      resourceQuantity: null,
    });
  }

  /**
   * Функция удаляет ресурс из списка по индексу.
   * @param index Индекс элемента в массиве.
   */
  public onRemoveResourceReceipt(index: number) {
    this.createResourceReceiptInput.includeResourceReceiptInputs?.splice(index, 1);
  }

  /**
   * Функция создает документ поступления.
   */
  public onCreateDocumentReceipt() {
    this._documentReceiptService.createResourceReceipt(this.createResourceReceiptInput).subscribe({
      next: (_) => {
        console.log('Документ поступления создан');
        this.onGetDocumentReceipts();
      },
      error: (err) => {
        if (err.status === 400) {
          if (
            err.error.message.includes('номером') ||
            err.error.message.includes('существует в системе')
          ) {
            this.serverNameError =
              err.error.message ||
              'Документ поступления с таким номером документа уже существует в системе.';
          } else {
            this.tableResourcesError = err.error.message || 'Ошибка в ресурсах поступления.';
          }

          this._cdr.detectChanges();
        }
        console.error('Ошибка создания документа поступления: ', err);
      },
    });
  }

  /**
   * Функция переходит к списку документов поступлений.
   */
  public onGetDocumentReceipts() {
    this._router.navigate(['/document-receipts']);
  }
}
