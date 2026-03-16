namespace WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt
{
    /// <summary>
    /// Класс входной модели редактирования ресурса поступления.
    /// </summary>
    public class ModifyResourceReceiptInput
    {
        /// <summary>
        /// Id ресурса поступления.
        /// </summary>
        public int ResourceReceiptId { get; set; }

        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int MeasureUnitId { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int ResourceQuantity { get; set; }
    }
}
