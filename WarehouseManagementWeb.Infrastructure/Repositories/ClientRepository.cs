using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
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

        #endregion

        #region Приватные методы.

        #endregion
    }
}
