using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentShipment
{
    /// <summary>
    /// Интерфейс репозитория документов отгрузок.
    /// </summary>
    public interface IDocumentShipmentRepository
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
        /// Метод проверяет существование документа отгрузки по номеру документа.
        /// </summary>
        /// <param name="documentShipmentNumberCode">Номер документа отгрузки.</param>
        /// <returns>Признак существования документа отгрузки.</returns>
        Task<bool> CheckDocumentShipmentExistsByNumberCodeAsync(string documentShipmentNumberCode);

        /// <summary>
        /// Метод проверяет существование документа отгрузки по номеру документа и Id.
        /// </summary>
        /// <param name="documentShipmentId">Id документа отгрузки.</param>
        /// <param name="documentShipmentNumberCode">Номер документа отгрузки.</param>
        /// <returns>Признак существования документа отгрузки.</returns>
        Task<bool> CheckDocumentShipmentExistsByIdAndNumberCodeAsync(int documentShipmentId,
            string documentShipmentNumberCode);

        /// <summary>
        ///  Метод создает документ отгрузки и добавляет ресурсы отгрузки.
        /// </summary>
        /// <param name="documentEntity">Модель документа отгрузки.</param>
        Task CreateResourceShipmentAsync(DocumentShipmentEntity documentEntity);

        /// <summary>
        /// Метод редактирует ресурсы отгрузки.
        /// </summary>    
        /// <param name="documentEntity">Модель документа отгрузки.</param>
        /// ресурсов поступлений.</param>
        Task UpdateResourceShipmentAsync(DocumentShipmentEntity documentEntity);

        /// <summary>
        /// Метод обновляет статус документу отгрузки.
        /// </summary>
        /// <param name="documentShipmentId">Id документа отгрузки.</param>
        /// <param name="statusEnum">Новый статус документа отгрузки.</param>
        Task ChangeStatusDocumentShipmentAsync(int documentShipmentId, DocumentStatusEnum statusEnum);

        /// <summary>
        /// Метод удаляет документ отгрузки.
        /// </summary>
        /// <param name="documentShipmentId">Id документа отгрузки.</param>
        Task RemoveDocumentShipmentAsync(int documentShipmentId);
    }
}
