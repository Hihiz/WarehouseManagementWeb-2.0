using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория документов поступлений.
    /// </summary>
    public class DocumentReceiptRepository : IDocumentReceiptRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public DocumentReceiptRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<ResourceReceiptListOutput>> GetResourceReceiptsAsync()
        {
            IEnumerable<ResourceReceiptListOutput> result = await _db.DocumentReceipts
                .AsNoTracking()
                .OrderByDescending(dr => dr.Date)
                .Select(dr => new ResourceReceiptListOutput
                {
                    DocumentReceiptId = dr.Id,
                    DocumentReceiptNumberCode = dr.NumberCode,
                    DocumentReceiptDate = dr.Date,
                    DocumentReceiptClientId = dr.ClientId,
                    DocumentReceiptClientName = dr.ClientEntity!.Name,
                    Items = dr.ResourceReceiptEntities!
                    .OrderByDescending(rr => rr.Id)
                    .Select(rr => new ResourceReceiptItemOutput
                    {
                        ResourceReceiptId = rr.Id,
                        ResourceId = rr.ResourceId,
                        ResourceTitle = rr.ResourceEntity!.Title,
                        MeasureUnitId = rr.MeasureUnitId,
                        MeasureUnitTitle = rr.MeasureUnitEntity!.Title,
                        ResourceQuantity = rr.Quantity
                    })

                })
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<ResourceReceiptListOutput?> GetResourceReceiptByDocumentReceiptIdAsync(
            int documentReceiptId)
        {
            ResourceReceiptListOutput? result = await _db.DocumentReceipts
                  .AsNoTracking()
                  .Where(dr => dr.Id == documentReceiptId)
                  .Select(dr => new ResourceReceiptListOutput
                  {
                      DocumentReceiptId = dr.Id,
                      DocumentReceiptNumberCode = dr.NumberCode,
                      DocumentReceiptDate = dr.Date,
                      DocumentReceiptClientId = dr.ClientId,
                      DocumentReceiptClientName = dr.ClientEntity!.Name,
                      Items = dr.ResourceReceiptEntities!
                      .OrderByDescending(rr => rr.Id)
                      .Select(rr => new ResourceReceiptItemOutput
                      {
                          ResourceReceiptId = rr.Id,
                          ResourceId = rr.ResourceId,
                          ResourceTitle = rr.ResourceEntity!.Title,
                          MeasureUnitId = rr.MeasureUnitId,
                          MeasureUnitTitle = rr.MeasureUnitEntity!.Title,
                          ResourceQuantity = rr.Quantity
                      })
                  }).FirstOrDefaultAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckDocumentReceiptExistsByNumberCodeAsync(string documentReceiptNumberCode)
        {
            bool result = await _db.DocumentReceipts
                .AsNoTracking()
                .AnyAsync(dr => dr.NumberCode == documentReceiptNumberCode);

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckDocumentReceiptExistsByIdAndNumberCodeAsync(int documentReceiptId,
            string documentReceiptNumberCode)
        {
            bool result = await _db.DocumentReceipts
               .AsNoTracking()
               .AnyAsync(dr => dr.NumberCode == documentReceiptNumberCode && dr.Id != documentReceiptId);

            return result;
        }

        /// <inheritdoc />
        public async Task CreateResourceReceiptAsync(DocumentReceiptEntity documentEntity)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                // Добавляем документ.
                await _db.DocumentReceipts.AddAsync(documentEntity);

                if (documentEntity.ResourceReceiptEntities is null ||
                    !documentEntity.ResourceReceiptEntities.Any())
                {
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return;
                }

                List<int> resourceIds = documentEntity.ResourceReceiptEntities
                    .Select(k => k.ResourceId)
                    .Distinct()
                    .ToList();

                List<int> measureUnitIds = documentEntity.ResourceReceiptEntities
                    .Select(k => k.MeasureUnitId)
                    .Distinct()
                    .ToList();

                Dictionary<(int, int), BalanceEntity> existingBalances = await _db.Balances
                    .Where(b => resourceIds.Contains(b.ResourceId) && measureUnitIds.Contains(b.MeasureUnitId))
                    .ToDictionaryAsync(
                            b => (b.ResourceId, b.MeasureUnitId),
                            b => b);

                foreach (var item in documentEntity.ResourceReceiptEntities)
                {
                    (int, int) key = (item.ResourceId, item.MeasureUnitId);

                    bool isExists = existingBalances.TryGetValue(key, out var balance);

                    // Если в балансе нет ресурса, то добавляем.
                    if (!isExists)
                    {
                        BalanceEntity newBalance = new BalanceEntity
                        {
                            ResourceId = item.ResourceId,
                            MeasureUnitId = item.MeasureUnitId,
                            Quantity = item.Quantity
                        };

                        await _db.Balances.AddAsync(newBalance);

                        existingBalances[key] = newBalance;
                    }

                    // Существующему ресурсу добавляем количество.
                    else if (isExists)
                    {
                        balance!.Quantity += item.Quantity;
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
        public async Task UpdateResourceReceiptAsync(DocumentReceiptEntity documentEntity)
        {
            DocumentReceiptEntity? entity = await _db.DocumentReceipts
                .Include(x => x.ResourceReceiptEntities)
                .FirstOrDefaultAsync(x => x.Id == documentEntity.Id);

            if (entity is null)
            {
                throw new InvalidOperationException("Ошибка при редактировании ресурсов поступления. " +
                                                   $"DocumentReceiptId: {documentEntity.Id}. " +
                                                   $"NumberCode: {documentEntity.NumberCode}.");
            }

            entity.NumberCode = documentEntity.NumberCode;
            entity.Date = documentEntity.Date;
            entity.ClientId = documentEntity.ClientId;

            List<int>? incomingIds = documentEntity.ResourceReceiptEntities!
                .Select(r => r.Id)
                .ToList();

            // Если исключили ресурсы, то удаляем их из поступления.
            List<ResourceReceiptEntity> resourcesToRemove = entity.ResourceReceiptEntities!
                .Where(r => !incomingIds.Contains(r.Id))
                .ToList();

            if (resourcesToRemove.Any())
            {
                foreach (var resource in resourcesToRemove)
                {
                    _db.ResourceReceipts.Remove(resource);
                }
            }

            // Обновляем существующие ресурсы или добавляем новые.
            foreach (var resourceReceipt in documentEntity.ResourceReceiptEntities!)
            {
                ResourceReceiptEntity? existResource = entity.ResourceReceiptEntities!
                    .FirstOrDefault(rr => rr.Id != 0 && rr.Id == resourceReceipt.Id);

                if (existResource is not null)
                {
                    // Обновляем существующие ресурсы.
                    existResource.ResourceId = resourceReceipt.ResourceId;
                    existResource.MeasureUnitId = resourceReceipt.MeasureUnitId;
                    existResource.Quantity = resourceReceipt.Quantity;
                }

                else
                {
                    // Если ресурс поступления не найден, то добавляем.
                    entity.ResourceReceiptEntities!.Add(new ResourceReceiptEntity
                    {
                        DocumentReceiptId = entity.Id,
                        ResourceId = resourceReceipt.ResourceId,
                        MeasureUnitId = resourceReceipt.MeasureUnitId,
                        Quantity = resourceReceipt.Quantity
                    });
                }
            }

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RemoveDocumentReceiptAsync(int documentReceiptId)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                await _db.ResourceReceipts
                    .Where(rr => rr.DocumentReceiptId == documentReceiptId)
                    .ExecuteDeleteAsync();

                await _db.DocumentReceipts
                    .Where(dr => dr.Id == documentReceiptId)
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