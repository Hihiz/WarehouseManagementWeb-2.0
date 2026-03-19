namespace WarehouseManagementWeb.Application.Dto.Output.ResourceShipment
{
    /// <summary>
    /// Класс выходной модели входящих ресурсов отгрузки.
    /// </summary>
    public class ResourceShipmentItemOutput
    {
        /// <summary>
        /// Id ресурса отгрузки.
        /// </summary>
        public int ResourceShipmentId { get; set; }

        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string? ResourceTitle { get; set; }

        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int MeasureUnitId { get; set; }

        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string? MeasureUnitTitle { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int ResourceQuantity { get; set; }
    }
}
