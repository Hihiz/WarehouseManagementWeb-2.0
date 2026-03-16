namespace WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt
{
    /// <summary>
    /// Класс входной модели редактирования ресурса поступления.
    /// </summary>
    public class UpdateResourceReceiptInput
    {
        /// <summary>
        /// Id документа поступления.
        /// </summary>
        public int DocumentReceiptId { get; set; }

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
        /// Список входящих ресурсов поступлений.
        /// </summary>
        public ICollection<ModifyResourceReceiptInput>? ModifyResourceReceiptInputs { get; set; }
    }
}
