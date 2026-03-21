using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс документа отгрузки сопоставляется с таблицей warehouse.document_shipments.
    /// </summary>
    public class DocumentShipmentEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер документа отгрузки.
        /// </summary>
        public string NumberCode { get; set; }

        /// <summary>
        /// Дата документа отгрузки.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// FK Id клиента документа отгрузки.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public ClientEntity? ClientEntity { get; set; }

        /// <summary>
        /// Статус документа отгрузки.
        /// </summary>
        public DocumentStatusEnum DocumentShipmentStatusEnum { get; set; }

        /// <summary>
        /// Список ресурсов отгрузки, которые связанны с документом отгрузки.
        /// </summary>
        public ICollection<ResourceShipmentEntity> ResourceShipmentEntities { get; set; }
    }
}
