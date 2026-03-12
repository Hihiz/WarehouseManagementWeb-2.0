using WarehouseManagementWeb.Application.Dto.Output.Resource;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.Resource
{
    /// <summary>
    /// Интерфейс репозитория ресурсов.
    /// </summary>
    public interface IResourceRepository
    {
        /// <summary>
        /// Метод получает список ресурсов.
        /// </summary>
        /// <returns>Список ресурсов.</returns>
        Task<IEnumerable<ResourceOutput>> GetResourcesAsync();

        /// <summary>
        /// Метод получает список активных ресурсов.
        /// </summary>
        /// <returns>Список активных ресурсов.</returns>
        Task<IEnumerable<ResourceOutput>> GetActiveResourcesAsync();

        /// <summary>
        /// Метод получает ресурс по Id.
        /// </summary>
        /// <param name="resourceId">Id ресурса.</param>
        /// <returns>Данные ресурса.</returns>
        Task<ResourceOutput?> GetResourceByIdAsync(int resourceId);
    }
}
