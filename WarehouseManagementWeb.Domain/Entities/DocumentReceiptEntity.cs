namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс документа поступления сопоставляется с таблицей warehouse.document_receipts.
    /// </summary>
    public class DocumentReceiptEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер документа поступления. 
        /// </summary>
        public string NumberCode { get; set; }

        /// <summary>
        /// Дата документа поступления.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// FK Id клиента документа поступления.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public ClientEntity? ClientEntity { get; set; }

        /// <summary>
        /// Список ресурсов поступлений, которые связанны с документом поступления.
        /// </summary>
        public ICollection<ResourceReceiptEntity>? ResourceReceiptEntities { get; set; }
    }
}
