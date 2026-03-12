using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс ресурсов сопоставляется с таблицей directory.resources.
    /// </summary>
    public class ResourceEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Статус ресурса в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum ResourceStatusEnum { get; set; }
    }
}
