namespace WarehouseManagementWeb.Application.Dto.Output.Client
{
    /// <summary>
    /// Класс выходной модели клиентов разделенных по статусам.
    /// </summary>
    public class ClientListByStatusOutput
    {
        /// <summary>
        /// Список активных клиентов.
        /// </summary>
        public IEnumerable<ClientOutput>? ActiveClients { get; set; }

        /// <summary>
        /// Список клиентов находящихся в архиве.
        /// </summary>
        public IEnumerable<ClientOutput>? ArchivedClients { get; set; }
    }
}
