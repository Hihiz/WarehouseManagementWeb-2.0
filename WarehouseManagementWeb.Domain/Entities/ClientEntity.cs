using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс клиента сопоставляется с таблицей directory.Clients.
    /// </summary>
    public class ClientEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование клиента.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес клиента.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Статус клиента в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum ClientStatusEnum { get; set; }
    }
}
