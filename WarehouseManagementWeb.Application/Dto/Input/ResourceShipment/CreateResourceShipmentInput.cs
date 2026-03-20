namespace WarehouseManagementWeb.Application.Dto.Input.ResourceShipment
{
    /// <summary>
    /// Класс входной модели создания ресурса отгрузки.
    /// </summary>
    public class CreateResourceShipmentInput
    {
        /// <summary>
        /// Номер документа отгрузки.
        /// </summary>
        public string? DocumentShipmentNumberCode { get; set; }

        /// <summary>
        /// Дата документа отгрузки.
        /// </summary>
        public DateTime DocumentShipmentDate { get; set; } 

        /// <summary>
        ///  Id клиента документа отгрузки.
        /// </summary>
        public int DocumentShipmentClientId { get; set; }

        /// <summary>
        /// Список входящих ресурсов в документ отгрузки.
        /// </summary>
        public ICollection<IncludeResourceShipmentInput>? IncludeResourceShipmentInputs { get; set; }
    }
}
