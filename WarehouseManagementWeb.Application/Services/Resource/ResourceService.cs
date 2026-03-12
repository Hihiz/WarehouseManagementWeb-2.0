using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.Resource;
using WarehouseManagementWeb.Application.Dto.Output.Resource;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Resource;
using WarehouseManagementWeb.Application.Interfaces.Services.Resource;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Services.Resource
{
    /// <summary>
    /// Класс реализует методы сервиса ресурсов.
    /// </summary>
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly ILogger<ResourceService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="resourceRepository">Репозиторий ресурсов.</param>
        /// <param name="logger">Логгер.</param>
        public ResourceService(IResourceRepository resourceRepository,
            ILogger<ResourceService> logger)
        {
            _resourceRepository = resourceRepository;
            _logger = logger;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<ResourceListByStatusOutput> GetResourcesAsync()
        {
            try
            {
                IEnumerable<ResourceOutput> resources = await _resourceRepository.GetResourcesAsync();

                List<ResourceOutput> activeResources = new List<ResourceOutput>(resources.Count(
                    r => r.ResourceStatusEnum == DirectoryStatusEnum.Active));

                List<ResourceOutput> archivedResources = new List<ResourceOutput>(resources.Count(
                    r => r.ResourceStatusEnum == DirectoryStatusEnum.Archived));

                foreach (var resource in resources)
                {
                    switch (resource.ResourceStatusEnum)
                    {
                        case DirectoryStatusEnum.Active:
                            activeResources.Add(resource);
                            break;

                        case DirectoryStatusEnum.Archived:
                            archivedResources.Add(resource);
                            break;
                    }
                }

                ResourceListByStatusOutput result = new ResourceListByStatusOutput()
                {
                    ActiveResources = activeResources.OrderByDescending(r => r.Id).ToList(),
                    ArchivedResources = archivedResources.OrderByDescending(r => r.Id).ToList()
                };

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceOutput>> GetActiveResourcesAsync()
        {
            try
            {
                IEnumerable<ResourceOutput> result = await _resourceRepository.GetActiveResourcesAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<ResourceOutput?> GetResourceByIdAsync(int resourceId)
        {
            try
            {
                if (resourceId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id ресурса. " +
                                                        $"ResourceId: {resourceId}.");
                }

                ResourceOutput? result = await _resourceRepository.GetResourceByIdAsync(resourceId);

                if (result is null)
                {
                    return new ResourceOutput();
                }

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task CreateResourceAsync(CreateResourceInput createResourceInput)
        {
            try
            {
                if (createResourceInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные ресурса.");
                }

                bool isResourceTitleExist = await _resourceRepository.CheckResourceExistsByTitleAsync(createResourceInput.Title!);

                if (isResourceTitleExist)
                {
                    throw new InvalidOperationException(
                        $"Ресурс с наименованием: '{createResourceInput.Title}' уже существует в системе.");
                }

                ResourceEntity entity = new ResourceEntity
                {
                    Title = createResourceInput.Title!
                };

                await _resourceRepository.CreateResourceAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task ChangeStatusResourceAsync(ChangeStatusResourceInput changeStatusResourceInput)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceAsync(UpdateResourceInput updateResourceInput)
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