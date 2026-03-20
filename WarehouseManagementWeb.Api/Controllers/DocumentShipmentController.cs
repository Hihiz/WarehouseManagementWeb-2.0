using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;
using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
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

        /// <summary>
        /// Метод получает список ресурсов отгрузки.
        /// </summary>
        /// <returns>Список ресурсов отгрузки.</returns>
        [HttpGet]
        [Route("document-shipments")]
        public async Task<IActionResult> GetResourceShipmentsAsync()
        {
            IEnumerable<ResourceShipmentListOutput> result = await _documentShipmentService
                .GetResourceShipmentsAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получает ресурс отгрузки по Id документа отгрузки.
        /// </summary>
        /// <param name="documentShipmentId">Id документа отгрузки.</param>
        /// <returns>Данные ресурса отгрузки.</returns>
        [HttpGet]
        [Route("document-shipment")]
        public async Task<IActionResult> GetResourceShipmentByDocumentShipmentIdAsync([FromQuery] int documentShipmentId)
        {
            ResourceShipmentListOutput? result = await _documentShipmentService
                .GetResourceShipmentByDocumentShipmentIdAsync(documentShipmentId);

            return Ok(result);
        }

        /// <summary>
        ///  Метод создает документ отгрузки и добавляет ресурсы отгрузки.
        /// </summary>
        /// <param name="input">Входная модель</param>
        [HttpPost]
        [Route("document-shipment")]
        public async Task<IActionResult> CreateResourceShipmentAsync([FromBody] CreateResourceShipmentInput input)
        {
            await _documentShipmentService.CreateResourceShipmentAsync(input);

            return Ok();
        }

        /// <summary>
        /// Метод редактирует ресурсы отгрузки.
        /// </summary>    
        /// <param name="input">Входная модель</param>
        [HttpPut]
        [Route("document-shipment")]
        public async Task<IActionResult> UpdateResourceShipmentAsync([FromBody] UpdateResourceShipmentInput input)
        {
            await _documentShipmentService.UpdateResourceShipmentAsync(input);

            return Ok();
        }

        /// <summary>
        /// Метод обновляет статус документу отгрузки.
        /// </summary>
        /// <param name="input">Входная модель</param>
        [HttpPatch]
        [Route("change-status-document-shipment")]
        public async Task<IActionResult> ChangeStatusDocumentShipmentAsync([FromBody] ChangeStatusDocumentShipmentInput
            input)
        {
            await _documentShipmentService.ChangeStatusDocumentShipmentAsync(input);

            return Ok();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
