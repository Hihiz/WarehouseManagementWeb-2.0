import { Component, OnInit } from '@angular/core';
import { ResourceReceiptListOutput } from '../../models/output/resource-receipt-list-output';
import { BehaviorSubject } from 'rxjs';
import { DocumentReceiptSerivce } from '../../services/document-receipt.serivce';;
import { Router } from '@angular/router';
import { AsyncPipe, DatePipe, NgClass } from '@angular/common';
/**
 * Класс компонента документов поступлений.
 */
@Component({
  selector: 'app-document-receipt.component',
  imports: [AsyncPipe, DatePipe],
  templateUrl: './document-receipt.component.html',
  styleUrl: './document-receipt.component.css',
})
export class DocumentReceiptComponent implements OnInit {
  public documentReceipts$ = new BehaviorSubject<ResourceReceiptListOutput[]>([]);

  /**
   * Конструктор.
   * @param _documentReceiptService Сервис документов поступлений.
   * @param _router Роутер.
   */
  constructor(
    private readonly _documentReceiptService: DocumentReceiptSerivce,
    private readonly _router: Router,
  ) {
    this.documentReceipts$ = this._documentReceiptService.documentReceipts$;
  }

  ngOnInit() {
  this.getResourceReceipts();
  }

  /**
   * Функция получает список ресурсов поступления.
   */
  private getResourceReceipts() {
    this._documentReceiptService.getResourceReceipts().subscribe((_) =>
        console.log('Получен список ресурсов поступления: ', this.documentReceipts$.value),
      );
  }

  /**
   * Функция получает ресурс поступления по Id документа поступления.
   * @param documentReceiptId Выбранный Id документа поступления.
   */
  public  onGetResourceReceiptByDocumentReceiptId(documentReceiptId: number) {
    this._router.navigate(['/detail-document-receipt'], {
      queryParams: {
        id: documentReceiptId,
      },
    });
  }

  /**
   * Функция переходит на страницу добавления ресурса поступления.
   */
  public onCreateResourceReceipt() {
    this._router.navigate(['/create-document-receipt']);
  }
}
