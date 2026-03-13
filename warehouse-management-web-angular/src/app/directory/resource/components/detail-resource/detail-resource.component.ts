import { AsyncPipe, CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ResourceService } from '../../services/resource.service';
import { ResourceOutput } from '../../models/output/resource-output';
import { BehaviorSubject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { UpdateResourceInput } from '../../models/input/update-resource-input';
import { ChangeStatusResourceInput } from '../../models/input/change-status-resource-input';
import { DirectoryStatusEnum } from '../../../enums/directory-status-enum';

/**
 * Класс компонента деталей ресурса.
 */
@Component({
  selector: 'app-detail-resource.component',
  imports: [FormsModule, CommonModule, AsyncPipe],
  templateUrl: './detail-resource.component.html',
  styleUrl: './detail-resource.component.css',
})
export class DetailResourceComponent implements OnInit {
  public detailResource$ = new BehaviorSubject<ResourceOutput>(new ResourceOutput());

  /**
   * Конструктор.
   * @param _resourceService Сервис ресурсов.
   * @param _activatedRoute Роутер строки запроса.
   * @param _router Роутер.
   * @param cdr Обнаружение изменений.
   */
  constructor(
    private readonly _resourceService: ResourceService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _router: Router,
    private cdr: ChangeDetectorRef,
  ) {
    this.detailResource$ = this._resourceService.detailResource$;
  }

  isLoader: boolean = true;
  serverNameError: string | null = null;
  updateResourceInput: UpdateResourceInput = new UpdateResourceInput();
  changeStatusResourceInput: ChangeStatusResourceInput = new ChangeStatusResourceInput();

  ngOnInit() {
    this.checkUrlParams();
  }

  /**
   * Функция берет значение из параметров URL.
   */
  private checkUrlParams() {
    this._activatedRoute.queryParams.subscribe((params) => {
      if (params['resourceId']) {
        this.updateResourceInput.id = params['resourceId'];
        this.getResourceById();
      }
    });
  }

  /**
   * Функция получает детали ресурса по Id.
   */
  private getResourceById() {
    this.isLoader = true;

    this._resourceService.getResourceById(this.updateResourceInput.id).subscribe((_) => {
      console.log('Детали ресурса ', this.detailResource$.value);

      this.updateResourceInput.title = this.detailResource$.value.title;

      this.isLoader = false;
    });
  }

  /**
   * Функция редактирует ресурс.
   */
  public onUpdateResource() {
    this.serverNameError = null;

    this._resourceService.updateResource(this.updateResourceInput).subscribe({
      next: (_) => {
        console.log('Ресурс обновлен');

        this.updateResourceInput = new UpdateResourceInput();

        // Актуализация списка ресурсов.
        this.onGetResources();
      },
      error: (err) => {
        if (err.status === 400) {
          this.serverNameError =
            err.error.message || 'Ресурс с таким наименованием уже существует в системе.';
          this.cdr.detectChanges();
        }
        console.error('Ошибка при редактировании ресурса:', err);
      },
    });
  }

  /**
   * Функция обновляет статус ресурса.
   */
  public async onChangeStatusResource() {
    if (this.updateResourceInput.id !== 0) {
      this.changeStatusResourceInput.resourceId = this.updateResourceInput.id;
    }
    switch (this.detailResource$.value.resourceStatusEnum.toString().toLowerCase()) {
      case 'active':
        this.changeStatusResourceInput.resourceStatusEnum = DirectoryStatusEnum.archived;
        break;

      case 'archived':
        this.changeStatusResourceInput.resourceStatusEnum = DirectoryStatusEnum.active;
        break;
    }

    this._resourceService.changeStatusResource(this.changeStatusResourceInput).subscribe((_) => {
      console.log('Статус ресурса изменен');

      this.onGetResources();
    });
  }

  /**
   * Функция удаляет клиента.
   */
  public onRemoveResource() {
    this._resourceService.removeResource(this.updateResourceInput.id).subscribe((_) => {
      console.log('Ресурс удален');

      this.onGetResources();
    });
  }

  /**
   * Функция переходит на страницу списка ресурсов.
   */
  public onGetResources() {
    this._router.navigate(['/resources']);
  }
}
