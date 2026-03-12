import { AsyncPipe, NgClass } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ResourceService } from '../../services/resource.service';
import { ResourceListByStatusOutput } from '../../models/output/resource-list-by-status-output';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

/**
 * Класс компонента ресурсов.
 */
@Component({
  selector: 'app-resource.component',
  imports: [NgClass, AsyncPipe],
  templateUrl: './resource.component.html',
  styleUrl: './resource.component.css',
})
export class ResourceComponent implements OnInit {
  public resources$ = new BehaviorSubject<ResourceListByStatusOutput>(
    new ResourceListByStatusOutput(),
  );

  /**
   * Конструктор.
   * @param _clientService Сервис клиентов.
   * @param _router Роутер.
   */
  constructor(
    private readonly _resourceService: ResourceService,
    private readonly _router: Router,
  ) {
    this.resources$ = this._resourceService.resources$;
  }

  isStatusResourcesActive: boolean = true;

  ngOnInit() {
    this.getResources();
  }

  /**
   * Функция переключает списки ресурсов по статусам.
   */
  public onSwitchResources() {
    this.isStatusResourcesActive = !this.isStatusResourcesActive;
    console.log('Состояние переключателя: ', this.isStatusResourcesActive);
  }

  /**
   * Функция получает список ресурсов.
   */
  private getResources() {
    this._resourceService
      .getResources()
      .subscribe((_) => console.log('Получен список ресурсов: ', this.resources$.value));
  }

  /**
   * Функция получает ресурс по Id.
   * @param resourceId Выбранный Id ресурса.
   */
  public onGetResourceById(resourceId: number) {
    this._router.navigate(['/detail-resource'], {
      queryParams: {
        resourceId: resourceId,
      },
    });
  }

  /**
   * Функция переходит на страницу добавления ресурса.
   */
  public onCreateResource() {
    this._router.navigate(['/create-resource']);
  }
}
