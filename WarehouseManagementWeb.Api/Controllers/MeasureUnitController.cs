using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Api.Validators.MeasureUnit;
using WarehouseManagementWeb.Application.Dto.Input.MeasureUnit;
using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Services.MeasureUnit;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Контроллер единиц измерений.
    /// </summary>
    [Authorize]
    [Route("api/directory/measure-unit")]
    [ApiController]
    public class MeasureUnitController : ControllerBase
    {
        private readonly IMeasureUnitService _measureUnitService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="measureUnitService">Сервис единиц измерений.</param>
        public MeasureUnitController(IMeasureUnitService measureUnitService)
        {
            _measureUnitService = measureUnitService;
        }

        #region Публичные методы.

        /// <summary>
        /// Метод получает список единиц измерений.
        /// </summary>
        /// <returns>Список единиц измерений.</returns>
        [HttpGet]
        [Route("measure-units")]
        public async Task<IActionResult> GetMeasureUnitsAsync()
        {
            MeasureUnitListByStatusOutput result = await _measureUnitService.GetMeasureUnitsAsync();

            return Ok(result);
        }

     
        #endregion

        #region Приватные методы.

        #endregion
    }
}
