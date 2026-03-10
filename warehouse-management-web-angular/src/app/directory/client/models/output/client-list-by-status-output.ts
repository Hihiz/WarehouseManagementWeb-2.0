import { ClientOutput } from './client-output';

/**
 * Класс выходной модели клиентов разделенных по статусам.
 */
export class ClientListByStatusOutput {
  /**
   * Список актвиных клиентов.
   */
  activeClients: ClientOutput[] = [];

  /**
   *Список клиентов находящихся в архиве.
   */
  archivedClients: ClientOutput[] = [];
}
