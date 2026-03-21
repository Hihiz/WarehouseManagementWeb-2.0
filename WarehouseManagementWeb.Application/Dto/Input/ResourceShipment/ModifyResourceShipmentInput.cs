namespace WarehouseManagementWeb.Application.Dto.Input.ResourceShipment
{
   /// <summary>
   /// Класс входной модели редактирования ресурса документа отгрузки.
   /// </summary>
    public class ModifyResourceShipmentInput : BaseResourceShipmentInput
    {
        /// <summary>
        /// Id ресурса отгрузки.
        /// </summary>
        public int ResourceShipmentId { get; set; }       
    }
}
