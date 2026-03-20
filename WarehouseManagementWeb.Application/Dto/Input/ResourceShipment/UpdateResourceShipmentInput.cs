namespace WarehouseManagementWeb.Application.Dto.Input.ResourceShipment
{
    /// <summary>
    /// Класс входной модели редактирования ресурсов документа отгрузки.
    /// </summary>
    public class UpdateResourceShipmentInput
    {       
        /// <summary>
        /// Id документа отгрузки.
        /// </summary>
        public int DocumentShipmentId { get; set; }

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
        public ICollection<ModifyResourceShipmentInput>? ModifyResourceShipmentInputs { get; set; }
    }
}
