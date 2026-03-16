namespace WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt
{
    /// <summary>
    /// Класс выходной модели ресурсов поступлений.
    /// </summary>
    public class ResourceReceiptListOutput
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
        public DateTime DocumentReceiptDate { get; set; }

        /// <summary>
        /// Наименование клиента документа поступения.
        /// </summary>
        public string? DocumentClientName { get; set; }

        /// <summary>
        ///  Список входящих ресурсов.
        /// </summary>
        public IEnumerable<ResourceReceiptItemOutput>? Items { get; set; }
    }
}
