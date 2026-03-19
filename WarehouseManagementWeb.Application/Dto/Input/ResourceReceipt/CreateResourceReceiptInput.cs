namespace WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt
{
    /// <summary>
    /// Класс входной модели создания ресурса поступления.
    /// </summary>
    public class CreateResourceReceiptInput
    {
        /// <summary>
        /// Номер документа поступления. 
        /// </summary>
        public string? DocumentReceiptNumberCode { get; set; }

        /// <summary>
        /// Дата документа поступления.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Id клиента документа поступления.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Список входящих ресурсов в документ поступления.
        /// </summary>
        public ICollection<IncludeResourceReceiptInput>? IncludeResourceReceiptInputs { get; set; }
    }
}
