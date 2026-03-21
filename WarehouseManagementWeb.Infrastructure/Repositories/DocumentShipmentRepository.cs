using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentShipment;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория документов отгрузок.
    /// </summary>
    public class DocumentShipmentRepository : IDocumentShipmentRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public DocumentShipmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceShipmentListOutput>> GetResourceShipmentsAsync()
        {
            IEnumerable<ResourceShipmentListOutput> result = await _db.DocumentShipments
                 .AsNoTracking()
                 .OrderByDescending(ds => ds.Date)
                 .Select(ds => new ResourceShipmentListOutput
                 {
                     DocumentShipmentId = ds.Id,
                     DocumentShipmenNumberCode = ds.NumberCode,
                     DocumentShipmentDate = ds.Date,
                     DocumentShipmentClientId = ds.ClientId,
                     DocumentShipmentClientName = ds.ClientEntity!.Name,
                     Items = ds.ResourceShipmentEntities!
                     .OrderByDescending(rr => rr.Id)
                     .Select(rs => new ResourceShipmentItemOutput
                     {
                         ResourceShipmentId = rs.Id,
                         ResourceId = rs.ResourceId,
                         ResourceTitle = rs.ResourceEntity!.Title,
                         MeasureUnitId = rs.MeasureUnitId,
                         MeasureUnitTitle = rs.MeasureUnitEntity!.Title,
                         ResourceQuantity = rs.Quantity
                     })
                 }).ToListAsync();

            return result!;
        }

        /// <inheritdoc />
        public async Task<ResourceShipmentListOutput?> GetResourceShipmentByDocumentShipmentIdAsync(
            int documentShipmentId)
        {
            ResourceShipmentListOutput? result = await _db.DocumentShipments
                  .AsNoTracking()
                  .Where(ds => ds.Id == documentShipmentId)
                  .Select(ds => new ResourceShipmentListOutput
                  {
                      DocumentShipmentId = ds.Id,
                      DocumentShipmenNumberCode = ds.NumberCode,
                      DocumentShipmentDate = ds.Date,
                      DocumentShipmentClientId = ds.ClientId,
                      DocumentShipmentClientName = ds.ClientEntity!.Name,
                      Items = ds.ResourceShipmentEntities!
                      .OrderByDescending(rr => rr.Id)
                      .Select(rs => new ResourceShipmentItemOutput
                      {
                          ResourceShipmentId = rs.Id,
                          ResourceId = rs.ResourceId,
                          ResourceTitle = rs.ResourceEntity!.Title,
                          MeasureUnitId = rs.MeasureUnitId,
                          MeasureUnitTitle = rs.MeasureUnitEntity!.Title,
                          ResourceQuantity = rs.Quantity
                      })
                  }).FirstOrDefaultAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckDocumentShipmentExistsByNumberCodeAsync(string documentShipmentNumberCode)
        {
            bool result = await _db.DocumentShipments
              .AsNoTracking()
              .AnyAsync(ds => ds.NumberCode == documentShipmentNumberCode);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckDocumentShipmentExistsByIdAndNumberCodeAsync(int documentShipmentId,
            string documentShipmentNumberCode)
        {
            bool result = await _db.DocumentShipments
                .AsNoTracking()
                .AnyAsync(ds => ds.Id != documentShipmentId && ds.NumberCode == documentShipmentNumberCode);

            return result;
        }

        /// <inheritdoc />
        public async Task CreateResourceShipmentAsync(DocumentShipmentEntity documentEntity)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                // Добавляем документ.
                await _db.DocumentShipments.AddAsync(documentEntity);

                foreach (var item in documentEntity.ResourceShipmentEntities)
                {
                    BalanceEntity? balance = await _db.Balances
                        .FirstOrDefaultAsync(b => b.ResourceId == item.ResourceId
                                            && b.MeasureUnitId == item.MeasureUnitId);

                    if (balance is null)
                    {
                        throw new InvalidOperationException($"Ресурс ID:{item.ResourceId} отсутствует на складе.");
                    }

                    // Вычитаем количество. 
                    balance.Quantity -= item.Quantity;
                    
                    if (balance.Quantity < 0)
                    {
                        throw new InvalidOperationException("Ошибка отгрузки: " +
                            "на складе недостаточное количество выбранного ресурса.");
                    }
                }

                await _db.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateResourceShipmentAsync(DocumentShipmentEntity documentEntity)
        {
            DocumentShipmentEntity? entity = await _db.DocumentShipments
                .Include(ds => ds.ResourceShipmentEntities)
                .FirstOrDefaultAsync(ds => ds.Id == documentEntity.Id);

            if (entity is null)
            {
                throw new InvalidOperationException("Ошибка при редактировании документа отгрузки. " +
                                                    $"DocumentShipmentId: {documentEntity.Id}. " +
                                                    $"NumberCode: {documentEntity.NumberCode}.");
            }

            entity.NumberCode = documentEntity.NumberCode;
            entity.Date = documentEntity.Date;
            entity.ClientId = documentEntity.ClientId;

            List<int> incomingIds = documentEntity.ResourceShipmentEntities
                .Select(rs => rs.Id)
                .ToList();

            // Если исключили ресурсы, то удаляем их из отгрузки.
            List<int> toResourceRemoveIds = entity.ResourceShipmentEntities
                .Where(rs => !incomingIds.Contains(rs.Id))
                .Select(rs => rs.Id)
                .ToList();

            if (toResourceRemoveIds.Any())
            {
                await _db.ResourceShipments
                    .Where(ds => toResourceRemoveIds.Contains(ds.Id))
                    .ExecuteDeleteAsync();
            }

            foreach (var resourceShipment in documentEntity.ResourceShipmentEntities)
            {
                ResourceShipmentEntity? existResource = entity.ResourceShipmentEntities
                    .FirstOrDefault(rs => rs.Id != 0 && rs.Id == resourceShipment.Id);

                if (existResource is not null)
                {
                    // Обновляем существующие ресурсы.
                    existResource.ResourceId = resourceShipment.ResourceId;
                    existResource.MeasureUnitId = resourceShipment.MeasureUnitId;
                    existResource.Quantity = resourceShipment.Quantity;
                }

                else
                {
                    // Если ресурс отгрузки не найден, то добавляем.
                    entity.ResourceShipmentEntities!.Add(new ResourceShipmentEntity
                    {
                        DocumentShipmentId = entity.Id,
                        ResourceId = resourceShipment.ResourceId,
                        MeasureUnitId = resourceShipment.MeasureUnitId,
                        Quantity = resourceShipment.Quantity
                    });
                }
            }

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task ChangeStatusDocumentShipmentAsync(int documentShipmentId, DocumentStatusEnum statusEnum)
        {
            DocumentShipmentEntity? entity = await _db.DocumentShipments
                .FirstOrDefaultAsync(ds => ds.Id == documentShipmentId);

            if (entity is null)
            {
                throw new InvalidOperationException("Ошибка при обновлении статуса документа отгрузки. " +
                                                   $"DocumentShipmentId: {documentShipmentId}. " +
                                                   $"Status: {statusEnum}.");
            }

            entity.DocumentShipmentStatusEnum = statusEnum;

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RemoveDocumentShipmentAsync(int documentShipmentId)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                await _db.ResourceShipments
                    .Where(rs => rs.DocumentShipmentId == documentShipmentId)
                    .ExecuteDeleteAsync();

                await _db.DocumentShipments
                    .Where(dr => dr.Id == documentShipmentId)
                    .ExecuteDeleteAsync();

                await transaction.CommitAsync();
            }

            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
