using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.Client;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
using WarehouseManagementWeb.Application.Interfaces.Services.Client;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Services.Client
{
    /// <summary>
    /// Класс реализует методы сервиса клиентов.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="clientRepository">Репозиторий клиентов.</param>
        /// <param name="logger">Логгер.</param>
        public ClientService(IClientRepository clientRepository,
            ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public Task ChangeStatusClientAsync(int clientId, DirectoryStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task CreateClientAsync(CreateClientInput createClientInput)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClientOutput>> GetActiveClientsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClientOutput?> GetClientByIdAsync(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<ClientListByStatusOutput> GetClientsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveClientAsync(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClientAsync(UpdateClientInput updateClientInput)
        {
            throw new NotImplementedException();
        }
    }
}