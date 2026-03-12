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

        /// <summary>
        /// Метод добавляет ресурс.
        /// </summary>
        /// <param name="createResourceInput">Входная модель.</param>
        Task CreateResourceAsync(CreateResourceInput createResourceInput);

        /// <summary>
        /// Метод редактирует ресурс.
        /// </summary>
        /// <param name="updateResourceInput">Входная модель.</param>
        Task UpdateResourceAsync(UpdateResourceInput updateResourceInput);

        /// <summary>
        /// Метод обновляет статус ресурсу.
        /// </summary>
        /// <param name="changeStatusResourceInput">Входная модель.</param>
        Task ChangeStatusResourceAsync(ChangeStatusResourceInput changeStatusResourceInput);

        /// <summary>
        /// Метод удаляет ресурс.
        /// </summary>
        /// <param name="resourceId">Id ресурса.</param>
        Task RemoveResourceAsync(int resourceId);
    }
}
