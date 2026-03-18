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

        /// <inheritdoc />
        public async Task<ResourceReceiptListOutput> GetResourceReceiptByDocumentReceiptIdAsync(int documentReceiptId)
        {
            try
            {
                if (documentReceiptId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id документа постуления. " +
                                                        $"DocumentReceiptId: {documentReceiptId}.");
                }

                ResourceReceiptListOutput result = await _documentReceiptRepository
                    .GetResourceReceiptByDocumentReceiptIdAsync(documentReceiptId);

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task CreateResourceReceiptAsync(CreateResourceReceiptInput input)
        {
            try
            {
                if (input is null)
                {
                    throw new InvalidOperationException("Недопустимые данные ресурсов поступления.");
                }

                bool isDocumentReceiptExist = await _documentReceiptRepository
                    .CheckDocumentReceiptExistsByNumberCodeAsync(input.DocumentReceiptNumberCode!);

                if (isDocumentReceiptExist)
                {
                    throw new InvalidOperationException(
                     $"Документ поступления с номером: '{input.DocumentReceiptNumberCode}' уже существует в системе.");
                }

                bool isDuplicates = input.IncludeResourceReceiptInputs!
                       .GroupBy(x => new { x.ResourceId, x.MeasureUnitId })
                       .Any(g => g.Count() > 1);

                if (isDuplicates)
                {
                    throw new InvalidOperationException("В документе не может быть дважды указан один и тот же ресурс с одинаковой единицей измерения.");
                }

                input.IncludeResourceReceiptInputs ??= new List<IncludeResourceReceiptInput>();

                DocumentReceiptEntity entity = new DocumentReceiptEntity
                {
                    NumberCode = input.DocumentReceiptNumberCode!,
                    Date = DateTime.SpecifyKind(input.Date, DateTimeKind.Utc),
                    ClientId = input.ClientId,
                    ResourceReceiptEntities = input.IncludeResourceReceiptInputs!
                    .Select(x => new ResourceReceiptEntity
                    {
                        ResourceId = x.ResourceId,
                        MeasureUnitId = x.MeasureUnitId,
                        Quantity = x.ResourceQuantity
                    }).ToList()
                };

                await _documentReceiptRepository.CreateResourceReceiptAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateResourceReceiptAsync(UpdateResourceReceiptInput input)
        {
            try
            {
                if (input is null)
                {
                    throw new InvalidOperationException("Недопустимые данные ресурсов поступления.");
                }

                bool isDocumentReceiptExist = await _documentReceiptRepository
                    .CheckDocumentReceiptExistsByIdAndNumberCodeAsync(input.DocumentReceiptId,
                    input.DocumentReceiptNumberCode!);

                if (isDocumentReceiptExist)
                {
                    throw new InvalidOperationException("Документ поступления с номером: " +
                        $"'{input.DocumentReceiptNumberCode}' уже существует в системе.");
                }

                DocumentReceiptEntity entity = new DocumentReceiptEntity
                {
                    Id = input.DocumentReceiptId,
                    NumberCode = input.DocumentReceiptNumberCode!,
                    Date = DateTime.SpecifyKind(input.Date, DateTimeKind.Utc),
                    ClientId = input.DocumentReceiptClientId,
                    ResourceReceiptEntities = input.ModifyResourceReceiptInputs!
                    .Select(x => new ResourceReceiptEntity
                    {
                        Id = x.ResourceReceiptId,
                        ResourceId = x.ResourceId,
                        MeasureUnitId = x.MeasureUnitId,
                        Quantity = x.ResourceQuantity
                    }).ToList()
                };

                await _documentReceiptRepository.UpdateResourceReceiptAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task RemoveDocumentReceiptAsync(int documentReceiptId)
        {
            try
            {
                if (documentReceiptId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id документа поступления. " +
                                                        $"DocumentReceiptId: {documentReceiptId}.");
                }

                await _documentReceiptRepository.RemoveDocumentReceiptAsync(documentReceiptId);
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
