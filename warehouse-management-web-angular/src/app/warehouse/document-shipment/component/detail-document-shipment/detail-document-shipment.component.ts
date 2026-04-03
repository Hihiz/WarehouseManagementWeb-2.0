import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BehaviorSubject, switchMap, tap } from 'rxjs';
import { ResourceShipmentListOutput } from '../../models/output/resource-shipment-list-output';
import { ClientOutput } from '../../../../directory/client/models/output/client-output';
import { BalanceOutput } from '../../../balance/models/output/balance-output';
import { AsyncPipe, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClientService } from '../../../../directory/client/services/client.service';
import { DocumentShipmentService } from '../../services/document-shipment.service';
import { BalanceService } from '../../../balance/services/balance.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DateService } from '../../../../helpers/date.service';
import { UpdateResourceShipmentInput } from '../../models/input/update-resource-shipment-input';
import { ModifyResourceShipmentInput } from '../../models/input/modify-resource-shipment-input';

/**
 * Класс компонента деталей документа отгрузки.
 */
@Component({
  selector: 'app-detail-document-shipment.component',
  imports: [FormsModule, NgClass, AsyncPipe],
  templateUrl: './detail-document-shipment.component.html',
  styleUrl: './detail-document-shipment.component.css',
})
export class DetailDocumentShipmentComponent implements OnInit {
  public detailDocumentShipment$ = new BehaviorSubject<ResourceShipmentListOutput>(
    new ResourceShipmentListOutput(),
  );
  public activeClients$ = new BehaviorSubject<ClientOutput[]>([]);
  public balances$ = new BehaviorSubject<BalanceOutput[]>([]);

  /**
   * Конструктор.
   * @param _clientService Сервис клиентов.
   * @param _documentShipmentService Сервис документов отгрузок.
   * @param _balanceService Сервис балансов.
   * @param _router Роутер.
   * @param _activateRouter Строка запрос.
   * @param _cdr Обнаруженеи изменений.
   * @param _dateService Серви даты.
   */
  constructor(
    private readonly _clientService: ClientService,
    private readonly _documentShipmentService: DocumentShipmentService,
    private readonly _balanceService: BalanceService,
    private readonly _router: Router,
    private readonly _activateRouter: ActivatedRoute,
    private readonly _cdr: ChangeDetectorRef,
    private readonly _dateService: DateService,
  ) {
    this.activeClients$ = this._clientService.activeClients$;
    this.balances$ = this._balanceService.balances$;
    this.detailDocumentShipment$ = this._documentShipmentService.detailDocumentShipment$;
  }

  isFormBlocked = false;
  isStatusActive: boolean = false;
  tableResourcesError: string | null = null;
  serverNameError: string | null = null;
  isLoader: boolean = true;
  updateDocumentShipmentInput: UpdateResourceShipmentInput = new UpdateResourceShipmentInput();

  ngOnInit() {
    this.checkUrlParams();

    this.getBalances()
      .pipe(
        switchMap(() => this.getResourceShipmentByDocumentShipmentId()),
        switchMap(() =>
          this.getActiveClients(this.updateDocumentShipmentInput.documentShipmentClientId),
        ),
      )
      .subscribe({
        next: () => {
          this.isLoader = false;
          this._cdr.detectChanges();
        },
        error: (err) => {
          console.error('Ошибка при загрузке данных: ', err);
          this.isLoader = false;
          this._cdr.detectChanges();
        },
      });
  }

  /**
   * Функция берет значение из параметров URL.
   */
  private checkUrlParams() {
    this._activateRouter.queryParams.subscribe((params) => {
      const id = params['id'];
      if (!id) {
        this._router.navigate(['/document-shipments']);
        return;
      }

      this.updateDocumentShipmentInput.documentShipmentId = id;
    });
  }

  /**
   * Функция возврвщает максимальное доступное количетсво баланса.
   * @param input Входная модель.
   * @returns Максимальное количество ресурса.
   */
  public getMaxQuantity(input: ModifyResourceShipmentInput): number {
    if (input._selectedBalance === null) {
      this.tableResourcesError = 'Ошибка: Баланс не выбран!';
      throw new Error('Ошибка: Баланс не выбран!');
    }

    const available = input._selectedBalance.availableQuantity;
    const original = input._startBalanceQuantity;
    return original + available;
  }

  /**
   * Функция получает ресурс отгрузки по Id документа отгрузки.
   * @returns Данные ресурса отгрузки.
   */
  private getResourceShipmentByDocumentShipmentId() {
    return this._documentShipmentService
      .getResourceShipmentByDocumentShipmentId(this.updateDocumentShipmentInput.documentShipmentId)
      .pipe(
        tap(() => {
          console.log('Детали документа отгрузки: ', this.detailDocumentShipment$.value);

          this.isStatusActive =
            this.detailDocumentShipment$.value.documentStatusEnum.toString() === 'Active';

          this.updateDocumentShipmentInput.documentShipmentNumberCode =
            this.detailDocumentShipment$.value.documentShipmentNumberCode;
          this.updateDocumentShipmentInput.documentShipmentDate = this._dateService.formatDate(
            this.detailDocumentShipment$.value.documentShipmentDate,
          );
          this.updateDocumentShipmentInput.documentShipmentClientId =
            this.detailDocumentShipment$.value.documentShipmentClientId;

          // Входящие ресурсы в отгрузку.
          this.updateDocumentShipmentInput.modifyResourceShipmentInputs =
            this.detailDocumentShipment$.value.items.map((item) => {
              // Ищем подходящий баланс по resourceId и measureUnitId
              let matchingBalance = this.balances$.value.find(
                (b) => b.resourceId === item.resourceId && b.measureUnitId === item.measureUnitId,
              );
              if (!matchingBalance) {
                this.tableResourcesError = `Ошибка: Баланс для ресурса Id ${item.resourceId} не найден!`;
                this.isFormBlocked = true;
                throw new Error(`Ошибка: Баланс для ресурса Id ${item.resourceId} не найден!`);
              }

              const result: ModifyResourceShipmentInput = {
                resourceShipmentId: item.resourceShipmentId,
                resourceId: item.resourceId,
                measureUnitId: item.measureUnitId,
                resourceQuantity: item.resourceQuantity,
                _selectedBalance: matchingBalance!,
                _balanceId: matchingBalance.id.toString(),
                _startBalanceQuantity: item.resourceQuantity,
              };

              return result;
            });
        }),
      );
  }

  /**
   * Фукнция получает список активных клиентов для заполнения выпадающего списка.
   * @returns Cписок активных клиентов.
   */
  private getActiveClients(clientId: number) {
    return this._clientService
      .getActiveClients(clientId)
      .pipe(
        tap(() => console.log('Получен список активных клиентов: ', this.activeClients$.value)),
      );
  }

  /**
   * Фукнция получает список баланса для заполнения выпадающего списка.
   * @returns Cписок активных клиентов.
   */
  private getBalances() {
    return this._balanceService
      .getAvailableBalances()
      .pipe(tap(() => console.log('Получен список баланса: ', this.balances$.value)));
  }

  /**
   * Функция добавляет выбранный ресурс из баланса в входную модель.
   * @param item Входная модель.
   * @param selectedBalanceId Выбранный id баланса.
   */
  public onBalanceChange(item: ModifyResourceShipmentInput, selectedBalanceId: string) {
    const balanceId: number = parseInt(selectedBalanceId);
    const findBalance = this.balances$.value.find((b) => b.id === balanceId);

    if (findBalance) {
      item.resourceId = findBalance.resourceId;
      item.measureUnitId = findBalance.measureUnitId;
      item._selectedBalance = findBalance;
    }

    console.log('test', item._selectedBalance?.availableQuantity);
  }

  /**
   * Функция добавляет пустую строку ресурса в список.
   */
  public onIncludeResourceShipment() {
    if (!this.updateDocumentShipmentInput.modifyResourceShipmentInputs) {
      this.updateDocumentShipmentInput.modifyResourceShipmentInputs = [];
    }

    // Добавляем новый обьект с начальными значениями.
    this.updateDocumentShipmentInput.modifyResourceShipmentInputs.push({
      resourceShipmentId: null,
      resourceId: null,
      measureUnitId: null,
      resourceQuantity: null,
      _selectedBalance: null,
      _balanceId: null,
      _startBalanceQuantity: 0,
    });
  }

  /**
   * Функция удаляет ресурс из списка по индексу.
   * @param index Индекс элемента в массиве.
   */
  public onExcludeResourceShipment(index: number) {
    this.updateDocumentShipmentInput.modifyResourceShipmentInputs?.splice(index, 1);
  }

  /**
   * Функция редактирует ресурс отгрузки.
   */
  public onUpdateResourceShipment(isSetActive: boolean = false) {
    this.serverNameError = null;
    if (this.updateDocumentShipmentInput.modifyResourceShipmentInputs.length === 0) {
      this.tableResourcesError = 'Выберите ресурсы';
      return;
    }

    this.updateDocumentShipmentInput.isSetActiveStatus = isSetActive;

    this._documentShipmentService
      .updateResourceShipment(this.updateDocumentShipmentInput)
      .subscribe({
        next: (_) => {
          console.log('Документ отгрузки обновлен');

          // Актуализация списка документов отгрузок.
          this.onGetResourceShipments();
        },
        error: (err) => {
          if (err.status === 400) {
            if (
              err.error.message.includes('номером') ||
              err.error.message.includes('существует в системе')
            ) {
              this.serverNameError =
                err.error.message ||
                'Документ отгрузки с таким номером документа уже существует в системе.';
            } else {
              this.tableResourcesError = err.error.message || 'Ошибка в ресурсах отгрузки.';
            }

            this._cdr.detectChanges();
          }
          console.error('Ошибка обновления документа отгрузки: ', err);
        },
      });
  }

  /**
   * Функция удаляет документ отгрузки.
   */
  public onRemoveDocumentShipment() {
    this._documentShipmentService
      .removeDocumentShipment(this.updateDocumentShipmentInput.documentShipmentId)
      .subscribe((_) => {
        console.log('Документ отгрузки удален');

        // Актуализация списка документов отгрузок.
        this.onGetResourceShipments();
      });
  }

  /**
   * Функция переходит на страницу списка документов отгрузок.
   */
  public onGetResourceShipments() {
    this._router.navigate(['/document-shipments']);
  }
}
