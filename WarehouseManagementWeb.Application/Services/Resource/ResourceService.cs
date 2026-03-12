using WarehouseManagementWeb.Application.Dto.Input.Resource;
using WarehouseManagementWeb.Application.Dto.Output.Resource;
using WarehouseManagementWeb.Application.Interfaces.Services.Resource;

namespace WarehouseManagementWeb.Application.Services.Resource
{
    /// <summary>
    /// Класс реализует методы сервиса ресурсов.
    /// </summary>
    public class ResourceService : IResourceService
    {
        public Task ChangeStatusResourceAsync(ChangeStatusResourceInput changeStatusResourceInput)
        {
            throw new NotImplementedException();
        }

        public Task CreateResourceAsync(CreateResourceInput createResourceInput)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceOutput>> GetActiveResourcesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResourceOutput?> GetResourceByIdAsync(int resourceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceOutput>> GetResourcesAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveResourceAsync(int resourceId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceAsync(UpdateResourceInput updateResourceInput)
        {
            throw new NotImplementedException();
        }
    }
}
