using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentShipment;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentShipment;

namespace WarehouseManagementWeb.Application.Services.DocumentShipment
{
    /// <summary>
    /// Класс реализует методы сервиса документов отгрузок.
    /// </summary>
    public class DocumentShipmentService : IDocumentShipmentService
    {
        private readonly IDocumentShipmentRepository _documentShipmentRepository;
        private readonly ILogger<DocumentShipmentService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="documentShipmentRepository">Репозиторий документов отгрузок</param>
        /// <param name="logger">Логгер.</param>
        public DocumentShipmentService(IDocumentShipmentRepository documentShipmentRepository,
            ILogger<DocumentShipmentService> logger)
        {
            _documentShipmentRepository = documentShipmentRepository;
            _logger = logger;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceShipmentListOutput>> GetResourceShipmentsAsync()
        {
            try
            {
                IEnumerable<ResourceShipmentListOutput> result = await _documentShipmentRepository
                    .GetResourceShipmentsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }


        /// <inheritdoc />
        public async Task<ResourceShipmentListOutput?> GetResourceShipmentByDocumentShipmentIdAsync(
            int documentShipmentId)
        {
            try
            {
                if (documentShipmentId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id документа отгрузки. " +
                                                        $"DocumentShipmentId: {documentShipmentId}.");
                }

                ResourceShipmentListOutput? result = await _documentShipmentRepository
                    .GetResourceShipmentByDocumentShipmentIdAsync(documentShipmentId);

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        public Task CreateResourceShipmentAsync(CreateResourceShipmentInput input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceShipmentAsync(UpdateResourceShipmentInput input)
        {
            throw new NotImplementedException();
        }

        public Task ChangeStatusDocumentShipmentAsync(ChangeStatusDocumentShipmentInput input)
        {
            throw new NotImplementedException();
        }

        public Task RemoveDocumentShipmentAsync(int documentShipmentId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
