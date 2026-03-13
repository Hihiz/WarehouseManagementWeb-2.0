import { ChangeDetectorRef, Component } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { Router } from '@angular/router';
import { CreateClientInput } from '../../models/input/create-client-input';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

/**
 *  Класс компонента добавления клиента.
 */
@Component({
  selector: 'app-create-client.component',
  imports: [FormsModule, CommonModule],
  templateUrl: './create-client.component.html',
  styleUrl: './create-client.component.css',
})
export class CreateClientComponent {
  /**
   * Конструктор.
   * @param _clientService Сервис клиентов.
   * @param _router Роутер.
   * @param cdr Обнаружение изменений.
   */
  constructor(
    private readonly _clientService: ClientService,
    private readonly _router: Router,
    private cdr: ChangeDetectorRef,
  ) {}

  serverNameError: string | null = null;
  createClientInput: CreateClientInput = new CreateClientInput();

  /**
   * Функция добавляет клиента.
   */
  public onCreateClient() {
    // Сбрасываем серверную ошибку перед новым запросом.
    this.serverNameError = null;

    console.log(this.createClientInput);
    this._clientService.createClient(this.createClientInput).subscribe({
      next: (_) => {
        console.log('Клиент добавлен');

        this.createClientInput = new CreateClientInput();

        // Актуализация списка клиентов.
        this.onGetClients();
      },
      error: (err) => {
        if (err.status === 400) {
          this.serverNameError =
            err.error.message || 'Клиент с таким наименованием уже существует в системе.';
          this.cdr.detectChanges();
        }
        console.error('Ошибка при создании клиента:', err);
      },
    });
  }

  /**
   * Функция переходит к списку клиентов.
   */
  public onGetClients() {
    this._router.navigate(['/clients']);
  }
}
