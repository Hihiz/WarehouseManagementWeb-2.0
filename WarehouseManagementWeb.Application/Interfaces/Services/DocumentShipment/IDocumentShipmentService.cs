using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
namespace WarehouseManagementWeb.Application.Interfaces.Services.DocumentShipment
{
    /// <summary>
    /// Интерфейс сервиса документов отгрузок.
    /// </summary>
    public interface IDocumentShipmentService
    {
        /// <summary>
        /// Метод получает список ресурсов отгрузки.
        /// </summary>
        /// <returns>Список ресурсов отгрузки.</returns>
        Task<IEnumerable<ResourceShipmentListOutput>> GetResourceShipmentsAsync();

        /// <summary>
        /// Метод получает ресурс отгрузки по Id документа отгрузки.
        /// </summary>
        /// <param name="documentShipmentId">Id документа отгрузки.</param>
        /// <returns>Данные ресурса отгрузки.</returns>
        Task<ResourceShipmentListOutput?> GetResourceShipmentByDocumentShipmentIdAsync(int documentShipmentId);

        /// <summary>
        ///  Метод создает документ отгрузки и добавляет ресурсы отгрузки.
        /// </summary>
        /// <param name="input">Входная модель</param>
        Task CreateResourceShipmentAsync(CreateResourceShipmentInput input);

        /// <summary>
        /// Метод редактирует ресурсы отгрузки.
        /// </summary>    
        /// <param name="input">Входная модель</param>      
        Task UpdateResourceShipmentAsync(UpdateResourceShipmentInput input);

        /// <summary>
        /// Метод удаляет документ отгрузки.
        /// </summary>
        /// <param name="documentShipmentId">Id документа отгрузки.</param>
        Task RemoveDocumentShipmentAsync(int documentShipmentId);
    }
}
