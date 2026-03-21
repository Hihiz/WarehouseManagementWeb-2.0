using WarehouseManagementWeb.Application.Dto.Output.Balance;

namespace WarehouseManagementWeb.Application.Interfaces.Services.Balance
{
    /// <summary>
    /// Интерфейс сервиса балансов.
    /// </summary>
    public interface IBalanceService
    {
        /// <summary>
        /// Метод получает список доступных ресурсов баланса.
        /// </summary>
        /// <returns>Список доступных ресурсов баланса.</returns>
        Task<IEnumerable<BalanceOutput>> GetAvailableBalancesAsync();
    }
}
