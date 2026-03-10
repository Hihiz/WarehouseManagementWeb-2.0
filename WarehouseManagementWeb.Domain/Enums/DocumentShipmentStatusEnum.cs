namespace WarehouseManagementWeb.Domain.Enums
{
    /// <summary>
    /// Перечисления статусов документа отгрузки.
    /// </summary>
    public enum DocumentShipmentStatusEnum
    {
        /// <summary>
        /// Неизвестный статус.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Статус подписанного документа отгрузки.
        /// </summary>      
        Active = 1,

        /// <summary>
        /// Статус не подписанного документа отгрузки.
        /// </summary>
        Inactive = 2
    }
}
