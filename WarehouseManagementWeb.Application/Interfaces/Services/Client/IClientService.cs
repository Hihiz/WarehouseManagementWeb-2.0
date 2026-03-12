using WarehouseManagementWeb.Application.Dto.Input.Client;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Interfaces.Services.Client
{
    /// <summary>
    /// Интерфейс сервиса клиентов.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Метод получает список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        Task<ClientListByStatusOutput> GetClientsAsync();

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
        /// Метод добавляет клиента.
        /// </summary>
        /// <param name="createClientInput">Входная модель.</param>
        Task CreateClientAsync(CreateClientInput createClientInput);

        /// <summary>
        /// Метод редактирует клиента.
        /// </summary>
        /// <param name="updateClientInput">Входная модель.</param>
        Task UpdateClientAsync(UpdateClientInput updateClientInput);

        /// <summary>
        /// Метод обновляет статус клиенту.
        /// </summary>
        /// <param name="changeStatusClientInput">Входная модель.</param>
        Task ChangeStatusClientAsync(ChangeStatusClientInput changeStatusClientInput);

        /// <summary>
        /// Метод удаляет клиента.
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        Task RemoveClientAsync(int clientId);
    }
}
