using WarehouseManagementWeb.Application.Dto.Output.Balance;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.Balance
{
    /// <summary>
    /// Интерфейс репозитория балансов.
    /// </summary>
    public interface IBalanceRepository
    {
        /// <summary>
        /// Метод получает список доступных ресурсов баланса.
        /// </summary>
        /// <returns>Список доступных ресурсов баланса.</returns>
        Task<IEnumerable<BalanceOutput>> GetAvailableBalancesAsync();
    }
}
