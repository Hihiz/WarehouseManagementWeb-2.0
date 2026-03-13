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

        /// <summary>
        /// Метод проверяет существование ресурса по его наименованию.
        /// </summary>
        /// <param name="resourceTitle">Наименование ресурса.</param>
        /// <returns>Признак существования ресурса.</returns>
        Task<bool> CheckResourceExistsByTitleAsync(string resourceTitle);

        /// <summary>
        /// Метод проверяет существование ресурса по его наименованию и Id ресурса.
        /// </summary>
        /// <param name="resourceId">Id ресурса.</param>
        /// <param name="resourceTitle">Наименование ресурса.</param>
        /// <returns>Признак существования ресурса.</returns>
        Task<bool> CheckResourceExistsByIdAndTitleAsync(int resourceId, string resourceTitle);

        /// <summary>
        /// Метод добавляет ресурс.
        /// </summary>
        /// <param name="resourceEntity">Модель ресурса.</param>
        Task CreateResourceAsync(ResourceEntity resourceEntity);

        /// <summary>
        /// Метод редактирует ресурс.
        /// </summary>
        /// <param name="resourceEntity">Модель ресурса.</param>
        Task UpdateResourceAsync(ResourceEntity resourceEntity);

        /// <summary>
        /// Метод обновляет статус ресурсу.
        /// </summary>
        /// <param name="resourceId">Id ресурса.</param>
        /// <param name="statusEnum">Новый статус ресурса.</param>
        Task ChangeStatusResourceAsync(int resourceId, DirectoryStatusEnum statusEnum);

        /// <summary>
        /// Метод удаляет ресурс.
        /// </summary>
        /// <param name="resourceId">Id ресурса.</param>
        Task RemoveResourceAsync(int resourceId);
    }
}
