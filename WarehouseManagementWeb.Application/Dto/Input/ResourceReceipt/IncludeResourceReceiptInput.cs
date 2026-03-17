namespace WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt
{
    /// <summary>
    /// Класс входной модели добавления ресурсов в поступление.
    /// </summary>
    public class IncludeResourceReceiptInput
    {
        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int? ResourceId { get; set; }

        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int? MeasureUnitId { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int? ResourceQuantity { get; set; }
    }
}
