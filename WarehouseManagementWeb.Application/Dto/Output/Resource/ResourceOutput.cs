using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Output.Resource
{
    /// <summary>
    /// Класс выходной модели ресурса.
    /// </summary>
    public class ResourceOutput
    {
        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Статус ресурса в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum ResourceStatusEnum { get; set; }
    }
}
