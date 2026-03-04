using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Output.Client
{
    /// <summary>
    /// Класс выходной модели клиентов.
    /// </summary>

    public class ClientOutput
    {
        /// <summary>
        /// Id клиента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование клиента.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Адрес клиента.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Статус клиента в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum ClientStatusEnum { get; set; }
    }
}
