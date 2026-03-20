using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;

namespace WarehouseManagementWeb.Application.Interfaces.Services.DocumentReceipt
{
    /// <summary>
    /// Интерфейс сервиса документов поступлений.
    /// </summary>
    public interface IDocumentReceiptService
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
        ///  Метод создает документ поступления и добавляет ресурсы поступления.
        /// </summary>
        /// <param name="input">Входная модель.</param>
        Task CreateResourceReceiptAsync(CreateResourceReceiptInput input);

        /// <summary>
        /// Метод редактирует ресурсы поступления.
        /// </summary>    
        /// <param name="input">Входная модель.</param>
        Task UpdateResourceReceiptAsync(UpdateResourceReceiptInput input);

        /// <summary>
        /// Метод удаляет документ поступления.
        /// </summary>
        /// <param name="documentReceiptId">Id документа поступления.</param>
        Task RemoveDocumentReceiptAsync(int documentReceiptId);
    }
}