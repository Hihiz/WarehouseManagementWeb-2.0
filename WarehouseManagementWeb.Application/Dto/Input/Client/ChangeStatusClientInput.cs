using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Input.Client
{
    /// <summary>
    /// Класс входной модели обновления статуса клиента.
    /// </summary>
    public class ChangeStatusClientInput
    {
        /// Id клиента.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Статус клиента.
        /// </summary>
        public DirectoryStatusEnum ClientStatusEnum { get; set; }
    }
}
