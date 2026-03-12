using WarehouseManagementWeb.Application.Dto.Output.Resource;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Resource;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория ресурсов.
    /// </summary>
    public class ResourceRepository : IResourceRepository
    {
        #region Публичные методы.

        public Task<IEnumerable<ResourceOutput>> GetResourcesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResourceOutput?> GetResourceByIdAsync(int resourceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceOutput>> GetActiveResourcesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckResourceExistsByIdAndTitleAsync(int resourceId, string resourceTitle)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckResourceExistsByTitleAsync(string resourceTitle)
        {
            throw new NotImplementedException();
        }

        public Task CreateResourceAsync(ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceAsync(ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public Task ChangeStatusResourceAsync(int resourceId, DirectoryStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task RemoveResourceAsync(int resourceId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
