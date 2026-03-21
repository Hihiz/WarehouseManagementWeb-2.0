using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс ресурса сопоставляется с таблицей directory.resources.
    /// </summary>
    public class ResourceEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Статус ресурса в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum ResourceStatusEnum { get; set; }

        /// <summary>
        /// Список ресурсов поступлений, которые связанны с ресурсом.
        /// </summary>
        public ICollection<ResourceReceiptEntity>? ResourceReceiptEntities { get; set; }

        /// <summary>
        /// Список ресурсов отгрузки, которые связанны с ресурсом.
        /// </summary>
        public ICollection<ResourceShipmentEntity>? ResourceShipmentEntities { get; set; }

        /// <summary>
        /// Список баланса, которые связанны с ресурсом.
        /// </summary>
        public ICollection<BalanceEntity>? BalanceEntities { get; set; }
    }
}
