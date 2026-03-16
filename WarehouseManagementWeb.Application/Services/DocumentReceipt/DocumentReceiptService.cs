using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentReceipt;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Application.Services.DocumentReceipt
{
    /// <summary>
    /// Класс реализует методы сервиса документов поступлений.
    /// </summary>
    public class DocumentReceiptService : IDocumentReceiptService
    {
        private readonly IDocumentReceiptRepository _documentReceiptRepository;
        private readonly ILogger<DocumentReceiptService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="documentReceiptRepository">Репозиторий документов поступлений</param>
        /// <param name="logger">Логгер.</param>
        public DocumentReceiptService(IDocumentReceiptRepository documentReceiptRepository,
            ILogger<DocumentReceiptService> logger)
        {

            _documentReceiptRepository = documentReceiptRepository;
            _logger = logger;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceReceiptListOutput>> GetResourceReceiptsAsync()
        {
            try
            {
                IEnumerable<ResourceReceiptListOutput> result = await _documentReceiptRepository
                    .GetResourceReceiptsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
