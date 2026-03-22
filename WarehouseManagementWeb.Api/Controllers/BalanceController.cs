using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Application.Interfaces.Services.Balance;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Класс контроллера балансов.
    /// </summary>
    [Authorize]
    [Route("api/warehouse/balance")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="balanceService">Сервис балансов.</param>
        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        #region Публичные методы.

        /// <summary>
        /// Метод получает список доступных ресурсов баланса.
        /// </summary>
        /// <returns>Список доступных ресурсов баланса.</returns>
        [HttpGet]
        [Route("balances")]
        public async Task<IActionResult> GetAvailableBalancesAsync() =>
            Ok(await _balanceService.GetAvailableBalancesAsync());

        #endregion

        #region Приватные методы.

        #endregion        
    }
}
