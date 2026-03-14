namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс ресурса поступления сопоставляется с таблицей warehouse.resource_receipts.
    /// </summary>
    public class ResourceReceiptEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK Id документа поступления.
        /// </summary>
        public int DocumentReceiptId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public DocumentReceiptEntity? DocumentReceiptEntity { get; set; }

        /// <summary>
        /// FK Id ресурса.
        /// </summary>
        public int? ResourceId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public ResourceEntity? ResourceEntity { get; set; }

        /// FK Id единицы измерения.
        /// </summary>
        public int? MeasureUnitId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public MeasureUnitEntity? MeasureUnitEntity { get; set; }

        /// <summary>
        /// Количество ресурсов поступления.
        /// </summary>
        public int? Quantity { get; set; }
    }
}
