using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Application.Dto.Output.Balance;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Balance;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует репозиторий балансов.
    /// </summary>
    public class BalanceRepository : IBalanceRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public BalanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<BalanceOutput>> GetAvailableBalancesAsync()
        {
            IEnumerable<BalanceOutput> result = await _db.Balances
                .OrderByDescending(b => b.Id)
                .Select(b => new BalanceOutput
                {
                    Id = b.Id,
                    ResourceId = b.ResourceId,
                    ResourceTitle = b.ResourceEntity!.Title,
                    MeasureUnitId = b.MeasureUnitId,
                    MeasureUnitTitle = b.MeasureUnitEntity!.Title,
                    AvailableQuantity = b.Quantity
                }).ToListAsync();

            return result;
        }

        #endregion

        #region Приватные методы.

        #endregion

    }
}
