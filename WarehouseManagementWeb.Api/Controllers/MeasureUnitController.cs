using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Application.Interfaces.Services.MeasureUnit;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Контроллер единиц измерений.
    /// </summary>
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

        #endregion

        #region Приватные методы.

        #endregion
    }
}
