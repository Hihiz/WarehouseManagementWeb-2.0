namespace WarehouseManagementWeb.Domain.Enums
{
    /// <summary>
    /// Перечисления статусов справочника.
    /// </summary>
    public enum DirectoryStatusEnum
    {
        /// <summary>
        /// Неизвестный статус.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Статус справочника в работе.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Статус справочника в архиве.
        /// </summary>
        Archived = 2,
    }
}
