using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Output.Balance;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Balance;
using WarehouseManagementWeb.Application.Interfaces.Services.Balance;

namespace WarehouseManagementWeb.Application.Services.Balance
{
    /// <summary>
    /// Класс реализует сервис балансов.
    /// </summary>
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly ILogger<BalanceService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="balanceRepository">Репозиторий балансов.</param>
        public BalanceService(IBalanceRepository balanceRepository,
           ILogger<BalanceService> logger)
        {
            _balanceRepository = balanceRepository;
            _logger = logger;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<BalanceOutput>> GetAvailableBalancesAsync()
        {
            try
            {
                return await _balanceRepository.GetAvailableBalancesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion

        #region Приватные методы.

        #endregion      
    }
}
