using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.Client
{
    /// <summary>
    /// Интерфейс репозитория клиентов.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Метод получает список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        Task<IEnumerable<ClientOutput>> GetClientsAsync();

        /// <summary>
        /// Метод получает список активных клиентов.
        /// </summary>
        /// <returns>Список активных клиентов.</returns>
        Task<IEnumerable<ClientOutput>> GetActiveClientsAsync();

        /// <summary>
        /// Метод получает клиента по Id.
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        /// <returns>Данные клиента.</returns>
        Task<ClientOutput?> GetClientByIdAsync(int clientId);

        /// <summary>
        /// Метод проверяет существование клиента по его наименованию. 
        /// </summary>
        /// <param name="clientName">Наименование клиента.</param>
        /// <returns>Признак существования клиента.</returns>
        Task<bool> CheckClientExistsByNameAsync(string clientName);

        /// <summary>
        /// Метод проверяет существование клиента по его наименованию и Id. 
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        /// <param name="clientName">Наименование клиента.</param>
        /// <returns>Признак существования клиента.</returns>
        Task<bool> CheckClientExistsByNameAndIdAsync(int clientId, string clientName);

        /// <summary>
        /// Метод добавляет клиента.
        /// </summary>
        /// <param name="clientEntity">Модель клиента.</param>
        Task CreateClientAsync(ClientEntity clientEntity);

        /// <summary>
        /// Метод редактирует клиента.
        /// </summary>
        /// <param name="clientEntity">Модель клиента.</param>
        Task UpdateClientAsync(ClientEntity clientEntity);

        /// <summary>
        /// Метод обновляет статус клиенту.
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        /// <param name="statusEnum">Новый статус клиента.</param>
        Task ChangeStatusClientAsync(int clientId, DirectoryStatusEnum statusEnum);

        /// <summary>
        /// Метод удаляет клиента.
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        Task RemoveClientAsync(int clientId);
    }
}
