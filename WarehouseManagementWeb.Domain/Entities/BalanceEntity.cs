namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс остатков ресурсов сопоставляется с таблицей warehouse.balances.
    /// </summary>
    public class BalanceEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK Id ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public ResourceEntity? ResourceEntity { get; set; }

        /// <summary>
        /// FK Id единицы измерения.
        /// </summary>
        public int MeasureUnitId { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public MeasureUnitEntity? MeasureUnitEntity { get; set; }

        /// <summary>
        /// Остаток ресурса.
        /// </summary>
        public int Quantity { get; set; }
    }
}
