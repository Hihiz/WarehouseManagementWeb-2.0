using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Services.Client;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Контроллер клиентов.
    /// </summary>
    [Authorize]
    [Route("api/directory/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="clientService">Сервис клиентов.</param>
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        #region Публичные методы.

        /// <summary>
        /// Метод получает список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        [HttpGet]
        [Route("clients")]
        public async Task<IActionResult> GetClientsAsync()
        {
            ClientListByStatusOutput result = await _clientService.GetClientsAsync();

            return Ok(result);
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}