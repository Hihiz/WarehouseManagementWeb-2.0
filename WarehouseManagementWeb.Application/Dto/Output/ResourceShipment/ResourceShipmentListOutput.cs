using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Output.ResourceShipment
{
    /// <summary>
    /// Класс выходной модели ресурса документа отгрузки.
    /// </summary>
    public class ResourceShipmentListOutput
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
        /// Id клиента документа отгрузки.
        /// </summary>
        public int DocumentShipmentClientId { get; set; }

        /// <summary>
        /// Наименование клиента документа отгрузки.
        /// </summary>
        public string? DocumentShipmentClientName { get; set; }

        /// <summary>
        /// Статус документа отгрузки в значении перечисления.
        /// </summary>
        public DocumentStatusEnum DocumentStatusEnum { get; set; }

        /// <summary>
        /// Список входящих ресурсов в документ отгрузки.
        /// </summary>
        public IEnumerable<ResourceShipmentItemOutput>? Items { get; set; }
    }
}
