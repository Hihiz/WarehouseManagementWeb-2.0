using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt
{
    /// <summary>
    /// Интерфейс репозитория документов поступлений.
    /// </summary>
    public interface IDocumentReceiptRepository
    {
        /// <summary>
        /// Метод получает список ресурсов поступления.
        /// </summary>
        /// <returns>Список ресурсов поступления.</returns>
        Task<IEnumerable<ResourceReceiptListOutput>> GetResourceReceiptsAsync();

        /// <summary>
        /// Метод получает ресурс поступления по Id документа поступления.
        /// </summary>
        /// <param name="documentReceiptId">Id документа поступления.</param>
        /// <returns>Данные ресурса поступления.</returns>
        Task<ResourceReceiptListOutput?> GetResourceReceiptByDocumentReceiptIdAsync(int documentReceiptId);

        /// <summary>
        /// Метод проверяет существование документа поступления по номеру документа.
        /// </summary>
        /// <param name="documentReceiptNumberCode">Номер документа поступления.</param>
        /// <returns>Признак существования документа поступления.</returns>
        Task<bool> CheckDocumentReceiptExistsByNumberCodeAsync(string documentReceiptNumberCode);

        /// <summary>
        /// Метод проверяет существование документа поступления по номеру документа и Id.
        /// </summary>
        /// <param name="documentReceiptId">Id документа поступления.</param>
        /// <param name="documentReceiptNumberCode">Номер документа поступления.</param>
        /// <returns>Признак существования документа поступления.</returns>
        Task<bool> CheckDocumentReceiptExistsByIdAndNumberCodeAsync(int documentReceiptId,
            string documentReceiptNumberCode);

        /// <summary>
        ///  Метод создает документ поступления и добавляет ресурсы поступления.
        /// </summary>
        /// <param name="documentEntity">Модель документа поступления.</param>
        Task CreateResourceReceiptAsync(DocumentReceiptEntity documentEntity);

        /// <summary>
        /// Метод редактирует ресурсы поступления.
        /// </summary>    
        /// <param name="documentEntity">Модель документа поступления.</param>
        /// ресурсов поступлений.</param>
        Task UpdateResourceReceiptAsync(DocumentReceiptEntity documentEntity);

        /// <summary>
        /// Метод удаляет документ поступления.
        /// </summary>
        /// <param name="documentReceiptId">Id документа поступления.</param>
        Task RemoveDocumentReceiptAsync(int documentReceiptId);
    }
}
