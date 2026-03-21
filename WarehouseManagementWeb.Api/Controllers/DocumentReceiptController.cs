using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentReceipt;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Класс контроллера документов поступлений.
    /// </summary>
    [Authorize]
    [Route("api/warehouse/document-receipt")]
    [ApiController]
    public class DocumentReceiptController : ControllerBase
    {
        private readonly IDocumentReceiptService _documentReceiptService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="documentReceiptService">Сервис документов поступлений.</param>
        public DocumentReceiptController(IDocumentReceiptService documentReceiptService)
        {
            _documentReceiptService = documentReceiptService;
        }

        #region Публичные методы.

        /// <summary>
        /// Метод получает список ресурсов поступления.
        /// </summary>
        /// <returns>Список ресурсов поступления.</returns
        [HttpGet]
        [Route("document-receipts")]
        public async Task<IActionResult> GetResourceReceiptsAsync()
        {
            IEnumerable<ResourceReceiptListOutput> result = await _documentReceiptService.GetResourceReceiptsAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получает ресурс поступления по Id документа поступления.
        /// </summary>
        /// <param name="documentReceiptId">Id документа поступления.</param>
        /// <returns>Данные ресурса поступления.</returns>
        [HttpGet]
        [Route("document-receipt")]
        public async Task<IActionResult> GetResourceReceiptByDocumentReceiptIdAsync([FromQuery] int documentReceiptId)
        {
            ResourceReceiptListOutput? result = await _documentReceiptService
                .GetResourceReceiptByDocumentReceiptIdAsync(documentReceiptId);

            return Ok(result);
        }

        /// <summary>
        ///  Метод создает документ поступления и добавляет ресурсы поступления.
        /// </summary>
        /// <param name="input">Входная модель.</param>
        [HttpPost]
        [Route("document-receipt")]
        public async Task<IActionResult> CreateResourceReceiptAsync([FromBody] CreateResourceReceiptInput input)
        {
            await _documentReceiptService.CreateResourceReceiptAsync(input);

            return Ok();
        }

        /// <summary>
        /// Метод редактирует ресурс поступления.
        /// </summary>    
        /// <param name="input">Входная модель.</param>
        [HttpPut]
        [Route("document-receipt")]
        public async Task<IActionResult> UpdateResourceReceiptAsync([FromBody] UpdateResourceReceiptInput input)
        {
            await _documentReceiptService.UpdateResourceReceiptAsync(input);

            return Ok();
        }

        /// <summary>
        /// Метод удаляет документ поступления.
        /// </summary>
        /// <param name="documentReceiptId">Id документа поступления.</param>
        [HttpDelete]
        [Route("document-receipt")]
        public async Task<IActionResult> RemoveDocumentReceiptAsync([FromBody] int documentReceiptId)
        {
            await _documentReceiptService.RemoveDocumentReceiptAsync(documentReceiptId);

            return Ok();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
