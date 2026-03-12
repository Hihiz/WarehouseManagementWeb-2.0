using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Application.Dto.Output.Resource
{
    /// <summary>
    /// Класс выходной модели ресурсов разделенных по статусам.
    /// </summary>
    public class ResourceListByStatusOutput
    {
        /// <summary>
        /// Список активных ресурсов.
        /// </summary>
        public IEnumerable<ResourceEntity>? ActiveResources { get; set; }

        /// <summary>
        /// Список ресурсов находящихся в архиве.
        /// </summary>
        public IEnumerable<ResourceEntity>? ArchivedResources { get; set; }
    }
}
