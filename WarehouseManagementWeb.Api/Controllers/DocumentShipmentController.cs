using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentShipment;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Класс контроллера документов отгрузок.
    /// </summary>
    [Authorize]
    [Route("api/warehouse/document-shipment")]
    [ApiController]
    public class DocumentShipmentController : ControllerBase
    {
        private readonly IDocumentShipmentService _documentShipmentService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="documentShipmentService">Сервис документов отгрузок.</param>
        public DocumentShipmentController(IDocumentShipmentService documentShipmentService)
        {
            _documentShipmentService = documentShipmentService;
        }

        #region Публичные методы.

        #endregion

        #region Приватные методы.

        #endregion
    }
}
