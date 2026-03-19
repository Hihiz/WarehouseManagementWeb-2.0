namespace WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt
{
    /// <summary>
    /// Класс входной модели редактирования ресурса поступления.
    /// </summary>
    public class ModifyResourceReceiptInput : BaseResourceReceiptInput
    {
        /// <summary>
        /// Id ресурса поступления.
        /// </summary>
        public int ResourceReceiptId { get; set; }      
    }
}
