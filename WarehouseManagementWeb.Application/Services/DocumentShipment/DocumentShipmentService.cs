using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentShipment;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentShipment;
using WarehouseManagementWeb.Domain.Entities;

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

        /// <inheritdoc />
        public async Task CreateResourceShipmentAsync(CreateResourceShipmentInput input)
        {
            try
            {
                if (input is null)
                {
                    throw new InvalidOperationException("Недопустимые данные ресурсов отгрузки.");
                }

                bool isDocumentShipmentExist = await _documentShipmentRepository
                    .CheckDocumentShipmentExistsByNumberCodeAsync(input.DocumentShipmentNumberCode!);

                if (isDocumentShipmentExist)
                {
                    throw new InvalidOperationException(
                     $"Документ отгрузки с номером: '{input.DocumentShipmentNumberCode}' уже существует в системе.");
                }

                IsDuplicateResourceShipments(input.IncludeResourceShipmentInputs!);

                input.IncludeResourceShipmentInputs ??= new List<IncludeResourceShipmentInput>();

                DocumentShipmentEntity entity = new DocumentShipmentEntity
                {
                    NumberCode = input.DocumentShipmentNumberCode!,
                    Date = DateTime.SpecifyKind(input.DocumentShipmentDate, DateTimeKind.Utc),
                    ClientId = input.DocumentShipmentClientId,
                    ResourceShipmentEntities = input.IncludeResourceShipmentInputs!
                    .Select(x => new ResourceShipmentEntity
                    {
                        ResourceId = x.ResourceId,
                        MeasureUnitId = x.MeasureUnitId,
                        Quantity = x.ResourceQuantity
                    }).ToList()
                };

                await _documentShipmentRepository.CreateResourceShipmentAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
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

        /// <summary>
        /// Метод проверяет дубликаты для ресурс + единица измрения.
        /// </summary>
        /// <param name="inputs">Список ресурсов.</param>
        /// <returns>Признак проверки.</returns>
        private bool IsDuplicateResourceShipments(IEnumerable<BaseResourceShipmentInput> inputs)
        {
            if (inputs is null || !inputs.Any())
            {
                return false; ;
            }

            bool isDuplicates = inputs
                      .GroupBy(x => new { x.ResourceId, x.MeasureUnitId })
                      .Any(g => g.Count() > 1);

            if (isDuplicates)
            {
                throw new InvalidOperationException(
                    "В документе не должно быть повторяющихся пар Ресурс + Единица измерения.");
            }

            return isDuplicates;
        }

        #endregion
    }
}
