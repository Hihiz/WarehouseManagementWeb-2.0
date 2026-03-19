namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс ресурса отгрузки сопоставляется с таблицей warehouse.resource_shipments.
    /// </summary>
    public class ResourceShipmentEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK Id документа отгрузки.
        /// </summary>
        public int DocumentShipmentId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public DocumentShipmentEntity? DocumentShipmentEntity { get; set; }

        /// <summary>
        /// FK Id ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public ResourceEntity? ResourceEntity { get; set; }

        /// FK Id единицы измерения.
        /// </summary>
        public int MeasureUnitId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public MeasureUnitEntity? MeasureUnitEntity { get; set; }

        /// <summary>
        /// Количество ресурсов отгрузки.
        /// </summary>
        public int Quantity { get; set; }
    }
}
