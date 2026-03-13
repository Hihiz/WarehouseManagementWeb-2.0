using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        

        #endregion

        #region Приватные методы.

        #endregion
    }
}
