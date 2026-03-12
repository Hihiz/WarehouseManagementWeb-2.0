import { ResourceOutput } from "./resource-output";

/**
 * Класс выходной модели ресурсов разделенных по статусам.
 */
export class ResourceListByStatusOutput {
  /**
   * Список активных ресурсов.
   */
  activeResources: ResourceOutput[] = [];

  /**
   *Список ресурсов находящихся в архиве.
   */
  archivedResources: ResourceOutput[] = [];
}
