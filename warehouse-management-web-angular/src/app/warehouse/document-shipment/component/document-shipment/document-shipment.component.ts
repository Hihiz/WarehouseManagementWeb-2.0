import { AsyncPipe, DatePipe, NgClass } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BalanceOutput } from '../../../balance/models/output/balance-output';
import { ResourceShipmentListOutput } from '../../models/output/resource-shipment-list-output';
import { DocumentShipmentService } from '../../services/document-shipment.service';
import { Router } from '@angular/router';
import { UpdateResourceShipmentInput } from '../../models/input/update-resource-shipment-input';

/**
 * Класс компонента документов отгрузок.
 */
@Component({
  selector: 'app-document-shipment.component',
  imports: [AsyncPipe, DatePipe, NgClass],
  templateUrl: './document-shipment.component.html',
  styleUrl: './document-shipment.component.css',
})
export class DocumentShipmentComponent implements OnInit {
  public balances$ = new BehaviorSubject<BalanceOutput[]>([]);
  public documentShipments$ = new BehaviorSubject<ResourceShipmentListOutput[]>([]);

  /**
   * Конструктор.
   * @param _documentShipmentService Сервис документов отгрузок.
   * @param _router Роутер.
   */
  constructor(
    private readonly _documentShipmentService: DocumentShipmentService,
    private readonly _router: Router,
    private readonly _cdr: ChangeDetectorRef,
  ) {
    this.documentShipments$ = this._documentShipmentService.documentShipments$;
  }

  countStatusActiveDocuments: number = 0;
  countStatusInActiveDocuments: number = 0;
  countDocuments: number = 0;

  isLoader: boolean = true;
  updateResourceShipmentInput: UpdateResourceShipmentInput = new UpdateResourceShipmentInput();

  ngOnInit() {
    this.getResourceShipments();
  }

  /**
   * Функция получает список ресурсов отгрузки.
   */
  private getResourceShipments() {
    this._documentShipmentService.getResourceShipments().subscribe(() => {
      console.log('Получен список ресурсов отгрузок: ', this.documentShipments$.value);

      this.isLoader = false;
      this.countDocuments = this.documentShipments$.value.length;
      this.countStatusActiveDocuments = this.documentShipments$.value
        .filter(d => d.documentStatusEnum.toString().toLowerCase() === 'active').length;
      this.countStatusInActiveDocuments = this.documentShipments$.value
        .filter(d => d.documentStatusEnum.toString().toLowerCase() === 'inactive').length;

      this._cdr.detectChanges();
    });
  }

  /**
   *  Функция получает ресурс отгрузки по Id документа отгрузки.
   * @param documentShipmentId Выбранный Id документа отгрузки.
   */
  public onGetResourceShipmentByDocumentShipmentId(documentShipmentId: number) {
    this._router.navigate(['/detail-document-shipment'], {
      queryParams: {
        id: documentShipmentId,
      },
    });
  }

  /**
   * Функция переходит на страницу добавления ресурса отгрузки.
   */
  public onCreateResourceShipment() {
    this._router.navigate(['/create-document-shipment']);
  }
}
