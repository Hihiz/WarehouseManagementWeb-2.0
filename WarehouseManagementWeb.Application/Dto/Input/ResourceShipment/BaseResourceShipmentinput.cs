namespace WarehouseManagementWeb.Application.Dto.Input.ResourceShipment
{
    /// <summary>
    /// Базовый класс входной модели ресурса документа отгрузки.
    /// </summary>
    public class BaseResourceShipmentinput
    {
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
        public int Quantity { get; set; }
    }
}
