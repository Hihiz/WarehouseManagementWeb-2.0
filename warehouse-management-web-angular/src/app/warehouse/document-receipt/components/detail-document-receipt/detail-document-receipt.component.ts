import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ResourceReceiptListOutput } from '../../models/output/resource-receipt-list-output';
import { BehaviorSubject } from 'rxjs';
import { DocumentReceiptSerivce } from '../../services/document-receipt.serivce';
import { ActivatedRoute, Router } from '@angular/router';
import { MeasureUnitOutput } from '../../../../directory/measure-unit/models/output/measure-unit-output';
import { ClientOutput } from '../../../../directory/client/models/output/client-output';
import { ResourceOutput } from '../../../../directory/resource/models/output/resource-output';
import { ResourceService } from '../../../../directory/resource/services/resource.service';
import { MeasureUnitService } from '../../../../directory/measure-unit/services/measure-unit.service';
import { ClientService } from '../../../../directory/client/services/client.service';
import { FormsModule } from '@angular/forms';
import { AsyncPipe, CommonModule } from '@angular/common';
import { UpdateResourceReceiptInput } from '../../models/input/update-resource-receipt-input';
import { ModifyResourceReceiptInput } from '../../models/input/modify-resource-receipt-input';

/**
 * Класс компонента деталей документа поступления.
 */
@Component({
  selector: 'app-detail-document-receipt.component',
  imports: [FormsModule, CommonModule, AsyncPipe],
  templateUrl: './detail-document-receipt.component.html',
  styleUrl: './detail-document-receipt.component.css',
})
export class DetailDocumentReceiptComponent implements OnInit {
  public detailDocumentReceipt$ = new BehaviorSubject<ResourceReceiptListOutput>(
    new ResourceReceiptListOutput(),
  );
  public activeMeasureUnits$ = new BehaviorSubject<MeasureUnitOutput[]>([]);
  public activeClients$ = new BehaviorSubject<ClientOutput[]>([]);
  public activeResources$ = new BehaviorSubject<ResourceOutput[]>([]);

  /**
   * Конструктор.
   * @param _activatedRoute Роутер строки запроса.
   * @param _router Роутер.
   * @param _documentReceiptService Сервис документов поступлений.
   * @param _measureUnitSerivce Сервис единиц измерений.
   * @param _clientService Сервис клиентов.
   * @param _resourceService Сервис ресурсов.
   */
  constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _router: Router,
    private readonly _documentReceiptService: DocumentReceiptSerivce,
    private readonly _measureUnitSerivce: MeasureUnitService,
    private readonly _clientService: ClientService,
    private readonly _resourceService: ResourceService,
    private _cdr: ChangeDetectorRef,
  ) {
    this.detailDocumentReceipt$ = this._documentReceiptService.detailDocumentReceipt$;
    this.activeMeasureUnits$ = this._measureUnitSerivce.activeMeasureUnits$;
    this.activeClients$ = this._clientService.activeClients$;
    this.activeResources$ = this._resourceService.activeResources$;
  }

  errorMessage: string | null = null;
  isLoader: boolean = true;
  serverNameError: string | null = null;
  updateResourceReceiptInput: UpdateResourceReceiptInput = new UpdateResourceReceiptInput();

  ngOnInit() {
    this.checkUrlParams();
  }

  /**
   * Функция берет значение из параметров URL.
   */
  private checkUrlParams() {
    this._activatedRoute.queryParams.subscribe((params) => {
      const id = params['id'];
      if (!id) {
        this._router.navigate(['document-receipts']);
      }

      this.updateResourceReceiptInput.documentReceiptId = id;

      this.getResourceReceiptByDocumentReceiptId();
      this.getActiveClients();
      this.getActiveMeasureUnits();
      this.getActiveResources();
    });
  }

  /**
   * Функция получает ресурс поступления по Id документа поступления.
   * @param documentReceiptId Id документа поступления.
   */
  private getResourceReceiptByDocumentReceiptId() {
    this.isLoader = true;

    this._documentReceiptService
      .getResourceReceiptByDocumentReceiptId(this.updateResourceReceiptInput.documentReceiptId)
      .subscribe((_) => {
        console.log('Детали документа поступления: ', this.detailDocumentReceipt$.value);

        this.updateResourceReceiptInput.documentReceiptNumberCode =
          this.detailDocumentReceipt$.value.documentReceiptNumberCode;

        this.updateResourceReceiptInput.date = new Date(
          this.detailDocumentReceipt$.value.documentReceiptDate,
        )
          .toISOString()
          .substring(0, 10) as any;

        this.updateResourceReceiptInput.documentReceiptClientId =
          this.detailDocumentReceipt$.value.documentReceiptClientId;

        // Входящие ресурсы.
        if (
          this.updateResourceReceiptInput.modifyResourceReceiptInputs === null &&
          this.detailDocumentReceipt$.value.items !== null
        )
          this.updateResourceReceiptInput.modifyResourceReceiptInputs =
            this.detailDocumentReceipt$.value.items?.map((item) => {
              let result: ModifyResourceReceiptInput = {
                resourceReceiptId: item.resourceReceiptId,
                resourceId: item.resourceId,
                measureUnitId: item.measureUnitId,
                resourceQuantity: item.resourceQuantity,
              };
              return result;
            });

        this.isLoader = false;
      });
  }

  /**
   * Функция добавляет пустую строку ресурса в список.
   */
  public onIncludeResourceReceipt() {
    if (!this.updateResourceReceiptInput.modifyResourceReceiptInputs) {
      this.updateResourceReceiptInput.modifyResourceReceiptInputs = [];
    }

    // Добавляем новый обьект с начальными значениями.
    this.updateResourceReceiptInput.modifyResourceReceiptInputs.push({
      resourceReceiptId: 0,
      resourceId: 0,
      measureUnitId: 0,
      resourceQuantity: 0,
    });
  }

  /**
   * Функция удаляет ресурс из списка по индексу.
   * @param index Индекс элемента в массиве.
   */
  public onRemoveResourceReceipt(index: number) {
    this.updateResourceReceiptInput.modifyResourceReceiptInputs?.splice(index, 1);
  }

  /**
   * Фукнция получает список активных ресурсов для заполнения выпадающего списка.
   */
  private getActiveResources() {
    this._resourceService.getActiveResources().subscribe((_) => {
      console.log('Получен список активных ресурсов: ', this.activeResources$.value);
    });
  }

  /**
   * Фукнция получает список активных единиц измерений для заполнения выпадающего списка.
   */
  private getActiveMeasureUnits() {
    this._measureUnitSerivce.getActiveMeasureUnits().subscribe((_) => {
      console.log('Получен список активных единиц измерений: ', this.activeMeasureUnits$.value);
    });
  }

  /**
   * Фукнция получает список активных клиентов для заполнения выпадающего списка.
   */
  private getActiveClients() {
    this._clientService.getActiveClients().subscribe((_) => {
      console.log('Получен список активных клиентов: ', this.activeClients$.value);
    });
  }

  /**
   *  Функция проверяет корректность выбранных значений ресурсов.
   * @returns Признак проверки.
   */
  private hasInvalidResources() {
  const items = this.updateResourceReceiptInput.modifyResourceReceiptInputs;
  
  if (!items || items.length === 0) return false;

  return items.some(item => 
    item.resourceReceiptId === 0 && (!item.resourceId || item.resourceId === 0)
  );
}

  /**
   * Функция редактирует ресурс поступления.
   */
  public onUpdateDocumentReceipt() {
    this.serverNameError = null;
    this.errorMessage = null;

if (this.hasInvalidResources()) {
    this.errorMessage = 'Пожалуйста, заполните все поля для добавленных ресурсов или удалите пустые строки.';
    return;
  }

    this._documentReceiptService.updateResourceReceipt(this.updateResourceReceiptInput).subscribe({
      next: (_) => {
        console.log('Документ поступления обновлен');

        this.updateResourceReceiptInput = new UpdateResourceReceiptInput();

        // Актуализация списка документов поступлений.
        this.onGetDocumentReceipts();
      },
      error: (err) => {
        if (err.status === 400) {
          this.serverNameError =
            err.error.message ||
            'Документ поступления с таким номером документа уже существует в системе.';
          this._cdr.detectChanges();
        }
        console.error('Ошибка обновления документа поступления: ', err);
      },
    });
  }

  /**
   * Функция удаляет документ поступления.
   */
  public onRemoveDocumentReceipt() {
    this._documentReceiptService
      .removeDocumentReceipt(this.updateResourceReceiptInput.documentReceiptId)
      .subscribe((_) => {
        console.log('Документ поступления удален');

        // Актуализация списка документов поступлений.
        this.onGetDocumentReceipts();
      });
  }

  /**
   * Функция переходит на страницу списка документов поступлений.
   */
  public onGetDocumentReceipts() {
    this._router.navigate(['/document-receipts']);
  }
}
