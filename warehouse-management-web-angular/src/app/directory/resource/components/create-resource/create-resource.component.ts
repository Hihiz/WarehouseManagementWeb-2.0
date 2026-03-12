import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ResourceService } from '../../services/resource.service';
import { Router } from '@angular/router';
import { CreateResourceInput } from '../../models/input/create-resource-input';

/**
 *  Класс компонента добавления ресурса.
 */
@Component({
  selector: 'app-create-resource.component',
  imports: [FormsModule, CommonModule],
  templateUrl: './create-resource.component.html',
  styleUrl: './create-resource.component.css',
})
export class CreateResourceComponent {
  /**
   * Конструктор.
   * @param _resourceService Сервис ресурсов.
   * @param _router Роутер.
   * @param cdr Обнаружение изменений.
   */
  constructor(
    private readonly _resourceService: ResourceService,
    private readonly _router: Router,
    private readonly cdr: ChangeDetectorRef,
  ) {}

  serverNameError: string | null = null;
  createResourceInput: CreateResourceInput = new CreateResourceInput();

  /**
   * Функция добавляет ресурс.
   */
  public onCreateResource() {
    this._resourceService.CreateResourceAsync(this.createResourceInput).subscribe({
      next: (_) => {
        console.log('Ресурс добавлен');
        this.createResourceInput = new CreateResourceInput();

        // Актуализация списка ресурсов.
        this.onGetResources();
      },
      error: (err) => {
        if (err.status === 400) {
          this.serverNameError =
            err.error.message || 'Ресурс с таким наименованием уже существует в системе.';
          this.cdr.detectChanges();
        }

        console.error('Ошибка при создании ресурса: ', err);
      },
    });
  }

  /**
   * Функция переходит к списку ресурсов.
   */
  public onGetResources() {
    this._router.navigate(['/resources']);
  }
}
