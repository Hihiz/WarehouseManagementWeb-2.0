namespace WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt
{
    /// <summary>
    /// Класс выходной модели входящих ресурсов поступления.
    /// </summary>
    public class ResourceReceiptItemOutput
    {
        /// <summary>
        /// Id ресурса поступления.
        /// </summary>
        public int ResourceReceiptId { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string? ResourceTitle { get; set; }

        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string? MeasureUnitTitle { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int? ResourceQuantity { get; set; }
    }
}
