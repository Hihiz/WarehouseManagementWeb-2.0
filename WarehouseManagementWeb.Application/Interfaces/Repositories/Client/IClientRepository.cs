using WarehouseManagementWeb.Application.Dto.Output.Client;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.Client
{
    /// <summary>
    /// Интерфейс репозитория клиента.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Метод получает список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        Task<IEnumerable<ClientOutput>> GetClientsAsync();
    }
}
