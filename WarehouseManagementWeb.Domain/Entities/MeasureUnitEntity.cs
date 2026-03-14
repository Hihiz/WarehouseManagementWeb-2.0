using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс единиц измерения сопоставляется с таблицей directory.measure_units.
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
    }
}
