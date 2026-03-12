using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Input.Resource
{
    /// <summary>
    /// Класс входной модели обновления статуса ресурса.
    /// </summary>
    public class ChangeStatusResourceInput
    {
        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Статус ресурса.
        /// </summary>
        public DirectoryStatusEnum ResourceStatusEnum { get; set; }
    }
}
