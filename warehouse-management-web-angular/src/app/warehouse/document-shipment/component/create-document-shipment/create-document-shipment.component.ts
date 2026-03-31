import { AsyncPipe, NgClass } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, forkJoin, tap } from 'rxjs';
import { ClientOutput } from '../../../../directory/client/models/output/client-output';
import { BalanceOutput } from '../../../balance/models/output/balance-output';
import { ClientService } from '../../../../directory/client/services/client.service';
import { BalanceService } from '../../../balance/services/balance.service';
import { Router } from '@angular/router';
import { DateService } from '../../../../helpers/date.service';
import { CreateResourceShipmentInput } from '../../models/input/create-resource-shipment-input';
import { DocumentShipmentService } from '../../services/document-shipment.service';
import { IncludeResourceShipmentInput } from '../../models/input/include-resource-shipment-input';

/**
 * Класс компонента создания документа отгрузки.
 */
@Component({
  selector: 'app-create-document-shipment.component',
  imports: [FormsModule, NgClass, AsyncPipe],
  templateUrl: './create-document-shipment.component.html',
  styleUrl: './create-document-shipment.component.css',
})
export class CreateDocumentShipmentComponent implements OnInit {
  public activeClients$ = new BehaviorSubject<ClientOutput[]>([]);
  public balances$ = new BehaviorSubject<BalanceOutput[]>([]);

  /**
   * Конструктор.
   * @param _clientService Сервис клиентов.
   * @param _balanceService Сервис балансов.
   * @param _router Роутер.
   */
  constructor(
    private readonly _clientService: ClientService,
    private readonly _documentShipmentService: DocumentShipmentService,
    private readonly _balanceService: BalanceService,
    private readonly _router: Router,
    private readonly _cdr: ChangeDetectorRef,
    private readonly _dateService: DateService,
  ) {
    this.activeClients$ = this._clientService.activeClients$;
    this.balances$ = this._balanceService.balances$;
  }

  tableResourcesError: string | null = null;
  isLoader: boolean = true;
  serverNameError: string | null = null;
  createResourceShipmentInput: CreateResourceShipmentInput = new CreateResourceShipmentInput();

  ngOnInit() {
    this.createResourceShipmentInput.documentShipmentDate = this._dateService.getDateNow();

    forkJoin([this.getActiveClients(), this.getBalances()]).subscribe({
      next: () => {
        this.isLoader = false;
        this._cdr.detectChanges();
      },
      error: (err) => {
        console.error('Ошибка при загрузке данные: ', err);
        this.isLoader = false;
      },
    });
  }

/**
   * Функция добавляет пустую строку ресурса в список.
   */
  public onIncludeResourceReceipt() {
    if (!this.createResourceShipmentInput.includeResourceShipmentInputs) {
      this.createResourceShipmentInput.includeResourceShipmentInputs = [];
    }

    // Добавляем новый обьект с начальными значениями.
    this.createResourceShipmentInput.includeResourceShipmentInputs.push({
      resourceId: null,
      measureUnitId: null,
      resourceQuantity: null,
      _selectedBalance: null,
      _balanceId: null
    });
  }

  /**
   * Функция удаляет ресурс из списка по индексу.
   * @param index Индекс элемента в массиве.
   */
  public onExcludeResourceReceipt(index: number) {
    this.createResourceShipmentInput.includeResourceShipmentInputs?.splice(index, 1);
  }

  /**
   * Фукнция получает список активных клиентов для заполнения выпадающего списка.
   * @returns Cписок активных клиентов.
   */
  private getActiveClients() {
    return this._clientService
      .getActiveClients()
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
      .pipe(
        tap(() => console.log('Получен список баланса: ', this.balances$.value)));
  }

  /**
   * Функция добавляет выбранный ресурс из баланса в входную модель.
   * @param item Входная модель.
   * @param selectedBalanceId Выбранный id баланса.
   */
  public onBalanceChange(item: IncludeResourceShipmentInput, selectedBalanceId: string) {
    const balanceId: number = parseInt(selectedBalanceId);
    const selectedBalance = this.balances$.value.find(b => b.id === balanceId);

    if (selectedBalance) {
      item.resourceId = selectedBalance.resourceId;
      item.measureUnitId = selectedBalance.measureUnitId;
      item._selectedBalance = selectedBalance;
      item._balanceId = selectedBalanceId;
    }
  }

  /**
   * Функция создает документ отгрузки.
   */
  public onCreateDocumentShipment(isSetActive: boolean = false) {

if (this.createResourceShipmentInput.includeResourceShipmentInputs.length === 0) {
  this.tableResourcesError = "Выберите ресурсы";
  return;
}

this.createResourceShipmentInput.isSetActiveStatus = isSetActive;

    this._documentShipmentService
      .createResourceShipment(this.createResourceShipmentInput)
      .subscribe({
        next: (_) => {
          console.log('Документ отгрузки создан');
          this.onGetDocumentShipments();
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
              this.tableResourcesError = err.error.message || 'Ошибка в ресурсах поступления.';
            }

            this._cdr.detectChanges();
          }
          console.error('Ошибка создания документа отгрузки: ', err);
        },
      });
  }

  /**
   * Функция переходит к списку документов отгрузок.
   */
  public onGetDocumentShipments() {
    this._router.navigate(['/document-shipments']);
  }
}
