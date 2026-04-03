using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Api.Validators.Clients;
using WarehouseManagementWeb.Application.Dto.Input.Client;
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

        /// <summary>
        /// Метод получает список активных клиентов.
        /// </summary>
        /// <returns>Список активных клиентов.</returns>
        [HttpGet]
        [Route("active-clients")]
        public async Task<IActionResult> GetActiveClientsAsync([FromQuery] int? clientId = null)
        {
            IEnumerable<ClientOutput> result = await _clientService.GetActiveClientsAsync(clientId);

            return Ok(result);
        }

        /// <summary>
        /// Метод получает клиента по Id.
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        /// <returns>Данные клиента.</returns>
        [HttpGet]
        [Route("client")]
        public async Task<IActionResult> GetClientByIdAsync([FromQuery] int clientId)
        {
            ValidationResult validator = await new GetClientByIdValidator().ValidateAsync(clientId);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            ClientOutput? result = await _clientService.GetClientByIdAsync(clientId);

            return Ok(result);
        }

        /// <summary>
        /// Метод добавляет клиента.
        /// </summary>
        /// <param name="createClientInput">Входная модель.</param>
        [HttpPost]
        [Route("client")]
        public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientInput createClientInput)
        {
            ValidationResult validator = await new CreateClientValidator().ValidateAsync(createClientInput);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _clientService.CreateClientAsync(createClientInput);

            return Ok();
        }

        /// <summary>
        /// Метод редактирует клиента.
        /// </summary>
        /// <param name="updateClientInput">Входная модель.</param>
        [HttpPut]
        [Route("client")]
        public async Task<IActionResult> UpdateClientAsync([FromBody] UpdateClientInput updateClientInput)
        {
            ValidationResult validator = await new UpdateClientValidator().ValidateAsync(updateClientInput);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _clientService.UpdateClientAsync(updateClientInput);

            return Ok();
        }

        /// <summary>
        /// Метод обновляет статус клиенту.
        /// </summary>
        /// <param name="changeStatusClientInput">Входная модель.</param>
        [HttpPatch]
        [Route("change-status-client")]
        public async Task<IActionResult> ChangeStatusClientAsync([FromBody] ChangeStatusClientInput
            changeStatusClientInput)
        {
            ValidationResult validator = await new ChangeStatusClientValidator().ValidateAsync(
                changeStatusClientInput);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _clientService.ChangeStatusClientAsync(changeStatusClientInput);

            return Ok();
        }

        /// <summary>
        /// Метод удаляет клиента.
        /// </summary>
        /// <param name="clientId">Id клиента.</param>
        [HttpDelete]
        [Route("client")]
        public async Task<IActionResult> RemoveClientAsync([FromBody] int clientId)
        {
            ValidationResult validator = await new RemoveClientValidator().ValidateAsync(clientId);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _clientService.RemoveClientAsync(clientId);

            return Ok();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}