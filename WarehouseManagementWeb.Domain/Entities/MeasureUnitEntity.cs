using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс единицы измерения сопоставляется с таблицей directory.measure_units.
    /// </summary>
    public class MeasureUnitEntity
    {

        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Статус единицы измерения в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum MeasureUnitStatusEnum { get; set; }

        /// <summary>
        /// Список ресурсов поступлений, которые связанны с единицей измерения.
        /// </summary>
        public ICollection<ResourceReceiptEntity>? ResourceReceiptEntities { get; set; }

        /// <summary>
        /// Список ресурсов отгрузки, которые связанны с единицей измерения.
        /// </summary>
        public ICollection<ResourceShipmentEntity>? ResourceShipmentEntities { get; set; }

        /// <summary>
        /// Список баланса, которые связанны с единицей измерения.
        /// </summary>
        public ICollection<BalanceEntity>? BalanceEntities { get; set; }
    }
}
