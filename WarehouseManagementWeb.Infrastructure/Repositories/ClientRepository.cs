using Microsoft.EntityFrameworkCore;
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

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<ClientOutput>> GetClientsAsync()
        {
            IEnumerable<ClientOutput> result = await _db.Clients
                .AsNoTracking()
                .Select(c => new ClientOutput
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    ClientStatusEnum = c.ClientStatusEnum,
                    ClientStatusTitle = c.ClientStatusEnum.ToString()
                })
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ClientOutput>> GetActiveClientsAsync()
        {
            IEnumerable<ClientOutput> result = await _db.Clients
                .AsNoTracking()
                .Where(c => c.ClientStatusEnum == DirectoryStatusEnum.Active)
                .Select(c => new ClientOutput
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    ClientStatusEnum = c.ClientStatusEnum,
                    ClientStatusTitle = c.ClientStatusEnum.ToString()
                })
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<ClientOutput?> GetClientByIdAsync(int clientId)
        {
            ClientOutput? result = await _db.Clients
                .Select(c => new ClientOutput
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    ClientStatusEnum = c.ClientStatusEnum,
                    ClientStatusTitle = c.ClientStatusEnum.ToString()
                }).FirstOrDefaultAsync(c => c.Id == clientId);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckClientExistsByNameAsync(string clientName)
        {
            bool result = await _db.Clients
                .AsNoTracking()
                .AnyAsync(c => c.Name == clientName);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckClientExistsByNameAndIdAsync(int clientId, string clientName)
        {            
            bool result = await _db.Clients
                  .AsNoTracking()
                  .AnyAsync(c => c.Name == clientName && c.Id != clientId);

            return result;
        }

        /// <inheritdoc />
        public async Task CreateClientAsync(ClientEntity clientEntity)
        {
            await _db.Clients.AddAsync(clientEntity);

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateClientAsync(ClientEntity clientEntity)
        {
            ClientEntity? client = await _db.Clients
                .FirstOrDefaultAsync(c => c.Id == clientEntity.Id);

            if (client is null)
            {
                throw new InvalidOperationException("Ошибка при редактировании клиента. " +
                                                    $"ClientId: {clientEntity.Id}. " +
                                                    $"Name: {clientEntity.Name}. " +
                                                    $"Address: {clientEntity.Address}.");
            }

            client.Name = clientEntity.Name;
            client.Address = clientEntity.Address;

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task ChangeStatusClientAsync(int clientId, DirectoryStatusEnum statusEnum)
        {
            ClientEntity? client = await _db.Clients
                .FirstOrDefaultAsync(c => c.Id == clientId);

            if (client is null)
            {
                throw new InvalidOperationException("Ошибка при обновлении статуса клиента. " +
                                                     $"ClientId: {clientId}. " +
                                                     $"Status: {statusEnum}.");
            }

            client.ClientStatusEnum = statusEnum;

            await _db.SaveChangesAsync();
        }


        #endregion

        #region Приватные методы.

        #endregion
    }
}
