namespace WarehouseManagementWeb.Application.Dto.Output.Balance
{
    /// <summary>
    /// Класс выходной модели баланса.
    /// </summary>
    public class BalanceOutput
    {
        /// <summary>
        /// Id баланса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string? ResourceTitle { get; set; }

        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int MeasureUnitId { get; set; }

        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string? MeasureUnitTitle { get; set; }

        /// <summary>
        /// Остаток ресурса.
        /// </summary>
        public int AvailableQuantity { get; set; }
    }
}
