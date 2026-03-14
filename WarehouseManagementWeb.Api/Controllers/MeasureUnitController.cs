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

        /// <summary>
        /// Метод получает список активных единиц измерения.
        /// </summary>
        /// <returns>Список активных единиц измерения.</returns>
        [HttpGet]
        [Route("active-measure-units")]
        public async Task<IActionResult> GetActiveMeasureUnitsAsync()
        {
            IEnumerable<MeasureUnitOutput> result = await _measureUnitService.GetActiveMeasureUnitsAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получает единицу измерения по Id.
        /// </summary>
        /// <param name="measureUnitId">Id единицы измерения.</param>
        /// <returns>Данные единицы измерения.</returns>
        [HttpGet]
        [Route("measure-unit")]
        public async Task<IActionResult> GetMeasureUnitByIdAsync([FromQuery] int measureUnitId)
        {
            ValidationResult validator = await new GetMeasureUnitByIdValidator().ValidateAsync(measureUnitId);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            MeasureUnitOutput? result = await _measureUnitService.GetMeasureUnitByIdAsync(measureUnitId);

            return Ok(result);
        }

        /// <summary>
        /// Метод добавляет единицу измерения.
        /// </summary>
        /// <param name="createMeasureUnitInput">Входная модель.</param>
        [HttpPost]
        [Route("measure-unit")]
        public async Task<IActionResult> CreateMeasureUnitAsync([FromBody] CreateMeasureUnitInput
            createMeasureUnitInput)
        {
            ValidationResult validator = await new CreateMeasureUnitValidator().ValidateAsync(createMeasureUnitInput);

            if (!validator.IsValid)
            {
                return BadRequest(string.Join("\n", validator.Errors));
            }

            await _measureUnitService.CreateMeasureUnitAsync(createMeasureUnitInput);

            return Ok();
        }


        #endregion

        #region Приватные методы.

        #endregion
    }
}
