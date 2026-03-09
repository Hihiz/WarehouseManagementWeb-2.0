using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    }
}