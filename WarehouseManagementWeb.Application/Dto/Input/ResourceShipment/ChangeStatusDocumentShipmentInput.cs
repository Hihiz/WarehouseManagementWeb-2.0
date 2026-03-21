using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Input.ResourceShipment
{
    /// <summary>
    /// Класс входной модели обновления статуса документа отгрузки. 
    /// </summary>
    public class ChangeStatusDocumentShipmentInput
    {
        /// <summary>
        /// Id документа отгрузки.
        /// </summary>
        public int DocumentShipmentId { get; set; }

        /// <summary>
        /// Статус документа отгрузки.
        /// </summary>
        public DocumentStatusEnum StatusEnum { get; set; }
    }
}
