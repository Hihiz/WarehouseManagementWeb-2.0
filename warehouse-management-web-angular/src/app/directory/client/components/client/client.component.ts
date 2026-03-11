import {  Component, OnInit } from '@angular/core';
import { ClientListByStatusOutput } from '../../models/output/client-list-by-status-output';
import { BehaviorSubject  } from 'rxjs';
import { ClientService } from '../../services/client.service';
import { Router } from '@angular/router';
import { AsyncPipe, NgClass } from '@angular/common';

/**
 * Класс компонента клиентов.
 */
@Component({
  selector: 'app-client.component',
  imports: [NgClass, AsyncPipe],
  templateUrl: './client.component.html',
  styleUrl: './client.component.css',
})
export class ClientComponent implements OnInit {
  public clients$ = new BehaviorSubject<ClientListByStatusOutput>(new ClientListByStatusOutput());

  /**
   * Конструктор.
   * @param _clientService Сервис клиентов.
   * @param _router Роутер.
   */
  constructor(
    private readonly _clientService: ClientService,
    private readonly _router: Router
  ) {
    this.clients$ = _clientService.clients$;
  }

  isStatusClientsActive: boolean = true;

  ngOnInit() {
    this.getClients();
  }

  /**
   * Функция переключает списки клиентов по статусам.
   */
  public onSwitchClients() {
    this.isStatusClientsActive = !this.isStatusClientsActive;
    console.log('Состояние переключателя: ', this.isStatusClientsActive);
  }

  /**
   * Функция получает список клиентов.
   */
  private getClients() {
    this._clientService
      .getClients()
      .subscribe((_) => console.log('Получен список клиентов: ', this.clients$.value));
  }

  /**
   * Функция получает клиента по Id.
   * @param clientId Выбранный Id клиента.
   */
  public onGetClientById(clientId: number) {
    this._router.navigate(['/detail-client'], {
      queryParams: {
        clientId: clientId,
      },
    });
  }

  /**
   * Функция переходит на страницу добавления клиента.
   */
  public onCreateClient() {
    this._router.navigate(['/create-client']);
  }
}
