using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Api.Validators.Resources;
using WarehouseManagementWeb.Application.Dto.Input.Resource;
using WarehouseManagementWeb.Application.Dto.Output.Resource;
using WarehouseManagementWeb.Application.Interfaces.Services.Resource;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Контроллер ресурсов.
    /// </summary>
    [Authorize]
    [Route("api/directory/resource")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="resourceService">Сервис ресурсов.</param>
        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        #region Публичные методы.

        /// <summary>
        /// Метод получает список ресурсов.
        /// </summary>
        /// <returns>Список ресурсов.</returns>
        [HttpGet]
        [Route("resources")]
        public async Task<IActionResult> GetResourcesAsync()
        {
            ResourceListByStatusOutput result = await _resourceService.GetResourcesAsync();

            return Ok(result);
        }

        /// <summary>7
        /// Метод получает список активных ресурсов.
        /// </summary>
        /// <returns>Список активных ресурсов.</returns>
        [HttpGet]
        [Route("active-resources")]
        public async Task<IActionResult> GetActiveResourcesAsync()
        {
            IEnumerable<ResourceOutput> result = await _resourceService.GetActiveResourcesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получает ресурс по Id.
        /// </summary>
        /// <param name="resourceId">Id ресурса.</param>
        /// <returns>Данные ресурса.</returns>
        [HttpGet]
        [Route("resource")]
        public async Task<IActionResult> GetResourceByIdAsync([FromQuery] int resourceId)
        {
            ValidationResult validator = await new GetResourceByIdValidator().ValidateAsync(resourceId);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            ResourceOutput? result = await _resourceService.GetResourceByIdAsync(resourceId);

            return Ok(result);
        }

        /// <summary>
        /// Метод добавляет ресурс.
        /// </summary>
        /// <param name="createResourceInput">Входная модель.</param>
        [HttpPost]
        [Route("resource")]
        public async Task<IActionResult> CreateResourceAsync([FromBody] CreateResourceInput createResourceInput)
        {
            ValidationResult validator = await new CreateResourceValidator().ValidateAsync(createResourceInput);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _resourceService.CreateResourceAsync(createResourceInput);

            return Ok();
        }

        /// <summary>
        /// Метод редактирует ресурс.
        /// </summary>
        /// <param name="updateResourceInput">Входная модель.</param>
        [HttpPut]
        [Route("resource")]
        public async Task<IActionResult> UpdateResourceAsync([FromBody] UpdateResourceInput updateResourceInput)
        {
            ValidationResult validator = await new UpdateResourceValidator().ValidateAsync(updateResourceInput);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _resourceService.UpdateResourceAsync(updateResourceInput);

            return Ok();
        }


        #endregion

        #region Приватные методы.

        #endregion
    }
}
