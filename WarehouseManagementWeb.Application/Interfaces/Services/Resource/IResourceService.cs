using WarehouseManagementWeb.Application.Dto.Input.Resource;
using WarehouseManagementWeb.Application.Dto.Output.Resource;

namespace WarehouseManagementWeb.Application.Interfaces.Services.Resource
{
    /// <summary>
    /// Интерфейс сервиса ресурсов.
    /// </summary>
    public interface IResourceService
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
