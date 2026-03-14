namespace WarehouseManagementWeb.Domain.Enums
{
    /// <summary>
    /// Перечисления статусов документа.
    /// </summary>
    public enum DocumentStatusEnum
    {
        /// <summary>
        /// Неизвестный статус.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Статус подписанного документа.
        /// </summary>      
        Active = 1,

        /// <summary>
        /// Статус не подписанного документа.
        /// </summary>
        Inactive = 2
    }
}
