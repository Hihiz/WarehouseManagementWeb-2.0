using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Application.Dto.Output.Resource;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Resource;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория ресурсов.
    /// </summary>
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public ResourceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceOutput>> GetResourcesAsync()
        {
            IEnumerable<ResourceOutput> result = await _db.Resources
                .AsNoTracking()
                 .OrderByDescending(r => r.Id)
                .Select(r => new ResourceOutput
                {
                    Id = r.Id,
                    Title = r.Title,
                    ResourceStatusEnum = r.ResourceStatusEnum
                })
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceOutput>> GetActiveResourcesAsync(int[]? resourceIds)
        {
            IEnumerable<ResourceOutput> result = await _db.Resources
                .AsNoTracking()
                .OrderByDescending(r => r.Id)
                .Where(r => r.ResourceStatusEnum == DirectoryStatusEnum.Active || 
                       resourceIds != null && resourceIds.Any() && resourceIds.Contains(r.Id))
                .Select(r => new ResourceOutput
                {
                    Id = r.Id,
                    Title = r.Title + (r.ResourceStatusEnum == DirectoryStatusEnum.Archived ? " (Архив)" : ""),
                    ResourceStatusEnum = r.ResourceStatusEnum
                })                
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<ResourceOutput?> GetResourceByIdAsync(int resourceId)
        {
            ResourceOutput? result = await _db.Resources
               .Select(r => new ResourceOutput
               {
                   Id = r.Id,
                   Title = r.Title,
                   ResourceStatusEnum = r.ResourceStatusEnum
               }).FirstOrDefaultAsync(r => r.Id == resourceId);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckResourceExistsByTitleAsync(string resourceTitle)
        {
            bool result = await _db.Resources
               .AsNoTracking()
               .AnyAsync(r => r.Title == resourceTitle);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckResourceExistsByIdAndTitleAsync(int resourceId, string resourceTitle)
        {
            bool result = await _db.Resources
                .AsNoTracking()
                .AnyAsync(r => r.Title == resourceTitle && r.Id != resourceId);

            return result;
        }

        /// <inheritdoc />
        public async Task CreateResourceAsync(ResourceEntity resourceEntity)
        {
            await _db.Resources.AddAsync(resourceEntity);

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateResourceAsync(ResourceEntity resourceEntity)
        {
            ResourceEntity? resource = await _db.Resources
                  .FirstOrDefaultAsync(c => c.Id == resourceEntity.Id);

            if (resource is null)
            {
                throw new InvalidOperationException("Ошибка при редактировании ресурса. " +
                                                    $"ResourceId: {resourceEntity.Id}. " +
                                                    $"Title: {resourceEntity.Title}.");
            }

            resource.Title = resourceEntity.Title;

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task ChangeStatusResourceAsync(int resourceId, DirectoryStatusEnum statusEnum)
        {
            ResourceEntity? resource = await _db.Resources
                   .FirstOrDefaultAsync(c => c.Id == resourceId);

            if (resource is null)
            {
                throw new InvalidOperationException("Ошибка при обновлении статуса ресурса. " +
                                                             $"ResourceId: {resourceId}. " +
                                                             $"Status: {statusEnum}.");
            }

            resource.ResourceStatusEnum = statusEnum;

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RemoveResourceAsync(int resourceId)
        {
            int removedResource = await _db.Resources
              .Where(x => x.Id == resourceId)
              .ExecuteDeleteAsync();

            if (removedResource <= 0)
            {
                throw new InvalidOperationException("Ошибка удаления ресурса. " +
                    $"ResourceId: {resourceId}.");
            }
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
