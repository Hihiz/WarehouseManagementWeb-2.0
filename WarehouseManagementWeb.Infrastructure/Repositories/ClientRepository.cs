using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
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

        #endregion

        #region Приватные методы.

        #endregion
    }
}
