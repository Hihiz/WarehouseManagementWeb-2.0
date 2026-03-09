using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория клиентов.
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public ClientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task ChangeStatusClientAsync(int clientId, DirectoryStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckClientExistsByNameAndIdAsync(int clientId, string clientName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckClientExistsByNameAsync(string clientName)
        {
            throw new NotImplementedException();
        }

        public Task CreateClientAsync(ClientEntity clientEntity)
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

        public Task<IEnumerable<ClientOutput>> GetClientsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveClientAsync(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClientAsync(ClientEntity clientEntity)
        {
            throw new NotImplementedException();
        }
    }
}
