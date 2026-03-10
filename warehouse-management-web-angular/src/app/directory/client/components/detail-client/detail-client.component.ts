import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ClientOutput } from '../../models/output/client-output';
import { ClientService } from '../../services/client.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UpdateClientInput } from '../../models/input/update-client-input';
import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';
import { ChangeStatusClientInput } from '../../models/input/change-status-client-input';
import { FormsModule } from '@angular/forms';
import { AsyncPipe, CommonModule } from '@angular/common';

/**
 * Класс компонента деталей клиента.
 */
@Component({
  selector: 'app-detail-client.component',
  imports: [FormsModule, CommonModule, AsyncPipe],
  templateUrl: './detail-client.component.html',
  styleUrl: './detail-client.component.css',
})
export class DetailClientComponent implements OnInit {
  public readonly detailClient$ = new BehaviorSubject<ClientOutput>(new ClientOutput());

  /**
   * Конструктор.
   * @param _clientService Сервис клиента.
   * @param _activatedRoute Роутер строки запроса.
   * @param _router Роутер.
   */
  constructor(
    private readonly _clientService: ClientService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _router: Router,
    private cdr: ChangeDetectorRef,
  ) {
    this.detailClient$ = this._clientService.detailClient$;
  }

  isLoader: boolean = true;
  serverNameError: string | null = null;
  updateClientInput: UpdateClientInput = new UpdateClientInput();
  changeStatusClientInput: ChangeStatusClientInput = new ChangeStatusClientInput();

  ngOnInit() {
    this.checkUrlParams();
  }

  /**
   * Функция берет значение из параметров URL.
   */
  private checkUrlParams() {
    this._activatedRoute.queryParams.subscribe((params) => {
      if (params['clientId']) {
        this.updateClientInput.id = params['clientId'];
        this.getClientById();
      }
    });
  }

  /**
   * Функция получает детали клиента по Id.
   */
  private getClientById() {
    this.isLoader = true;

    this._clientService.getClientById(this.updateClientInput.id).subscribe((_) => {
      console.log('Детали клиента ', this.detailClient$.value);

      this.updateClientInput.name = this.detailClient$.value.name;
      this.updateClientInput.address = this.detailClient$.value.address;

      this.isLoader = false;
    });
  }

  /**
   * Функция редактирует клиента.
   */
  public onUpdateClient() {
    this.serverNameError = null;

    this._clientService.updateClient(this.updateClientInput).subscribe({
      next: (_) => {
        console.log('Клиент обновлен');

        this.updateClientInput = new UpdateClientInput();

        // Актуализация списка клиентов.
        this.onGetClients();
      },
      error: (err) => {
        if (err.status === 400) {
          this.serverNameError =
            err.error.message || 'Клиент с таким наименованием уже существует в системе.';
          this.cdr.detectChanges();
        }
        console.error('Ошибка при редактировании клиента:', err);
      },
    });
  }

  /**
   * Функция обновляет статус клиенту.
   * @param statusEnum Статус.
   */
  public async onChangeStatusClient() {
    if (this.updateClientInput.id !== 0) {
      this.changeStatusClientInput.clientId = this.updateClientInput.id;
    }
    switch (this.detailClient$.value.clientStatusEnum.toString().toLowerCase()) {
      case 'active':
        this.changeStatusClientInput.clientStatusEnum = DirectoryStatusEnum.archived;
        break;

      case 'archived':
        this.changeStatusClientInput.clientStatusEnum = DirectoryStatusEnum.active;
        break;
    }
    console.log(this.changeStatusClientInput.clientStatusEnum);
    this._clientService.changeStatusClient(this.changeStatusClientInput).subscribe((_) => {
      console.log('Статус клиента изменен');

      this.onGetClients();
    });
  }

  /**
   * Функция удаляет клиента.
   */
  public onRemoveClient() {
    this._clientService.removeClient(this.updateClientInput.id).subscribe((_) => {
      console.log('Клиент удален');

      this.onGetClients();
    });
  }

  /**
   * Функция переходит на страницу списка клиентов.
   */
  public onGetClients() {
    this._router.navigate(['/clients']);
  }
}
