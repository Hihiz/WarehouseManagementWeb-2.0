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
                     DocumentShipmentNumberCode = ds.NumberCode,
                     DocumentShipmentDate = ds.Date,
                     DocumentShipmentClientId = ds.ClientId,
                     DocumentShipmentClientName = ds.ClientEntity!.Name,
                     DocumentStatusEnum = ds.DocumentShipmentStatusEnum,
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
                      DocumentShipmentNumberCode = ds.NumberCode,
                      DocumentShipmentDate = ds.Date,
                      DocumentShipmentClientId = ds.ClientId,
                      DocumentShipmentClientName = ds.ClientEntity!.Name,
                      DocumentStatusEnum = ds.DocumentShipmentStatusEnum,
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

                List<int> resourceIds = documentEntity.ResourceShipmentEntities
                    .Select(k => k.ResourceId)
                    .Distinct()
                    .ToList();

                List<int> measureUnitIds = documentEntity.ResourceShipmentEntities
                    .Select(k => k.MeasureUnitId)
                    .Distinct()
                    .ToList();

                Dictionary<(int, int), BalanceEntity> existingBalances = await _db.Balances
                    .Where(b => resourceIds.Contains(b.ResourceId) && measureUnitIds.Contains(b.MeasureUnitId))
                    .ToDictionaryAsync(
                            b => (b.ResourceId, b.MeasureUnitId),
                            b => b);

                foreach (var item in documentEntity.ResourceShipmentEntities)
                {
                    (int, int) key = (item.ResourceId, item.MeasureUnitId);

                    bool isExists = existingBalances.TryGetValue(key, out var balance);

                    if (!isExists)
                    {
                        throw new InvalidOperationException($"Ресурс Id:{item.ResourceId} отсутствует на складе.");
                    }

                    // Вычитаем количество. 
                    balance!.Quantity -= item.Quantity;

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
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
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

                if (documentEntity.DocumentShipmentStatusEnum is not DocumentStatusEnum.Undefined)
                {
                    entity.DocumentShipmentStatusEnum = documentEntity.DocumentShipmentStatusEnum;
                }

                List<int> incominResourceIds = documentEntity.ResourceShipmentEntities
                 .Select(k => k.ResourceId)
                 .Distinct()
                 .ToList();

                List<int> incominMeasureUnitIds = documentEntity.ResourceShipmentEntities
                    .Select(k => k.MeasureUnitId)
                    .Distinct()
                    .ToList();

                List<int> existingResourceIds = entity.ResourceShipmentEntities
                    .Select(k => k.ResourceId)
                    .Distinct()
                    .ToList();

                List<int> existingMeasureUnitIds = entity.ResourceShipmentEntities
                   .Select(k => k.MeasureUnitId)
                   .Distinct()
                   .ToList();

                List<int> allResourceIds = incominResourceIds.Union(existingResourceIds)
                    .ToList();
                List<int> allMeasureUnitIds = incominMeasureUnitIds.Union(existingMeasureUnitIds)
                  .ToList();

                Dictionary<(int, int), BalanceEntity> balanceDict = await _db.Balances
                    .Where(b => allResourceIds.Contains(b.ResourceId) &&
                                allMeasureUnitIds.Contains(b.MeasureUnitId))
                    .ToDictionaryAsync(b => (b.ResourceId, b.MeasureUnitId), b => b);

                List<int> incomingResourceshipmentIds = documentEntity.ResourceShipmentEntities
                    .Select(rs => rs.Id)
                    .ToList();

                // Если исключили ресурсы, то удаляем их из отгрузки.
                List<ResourceShipmentEntity> resourcesToRemove = entity.ResourceShipmentEntities
                    .Where(rs => !incomingResourceshipmentIds.Contains(rs.Id))
                    .ToList();

                if (resourcesToRemove.Any())
                {
                    foreach (var resource in resourcesToRemove)
                    {
                        // Вычитаем количество входящих ресурсов в поступление.
                        BalanceEntity balance = GetExistingBalance(balanceDict, resource.ResourceId,
                            resource.MeasureUnitId);

                        balance.Quantity += resource.Quantity;

                        _db.ResourceShipments.Remove(resource);
                    }
                }

                foreach (var resourceShipment in documentEntity.ResourceShipmentEntities)
                {
                    ResourceShipmentEntity? existResource = entity.ResourceShipmentEntities
                        .FirstOrDefault(rs => rs.Id != 0 && rs.Id == resourceShipment.Id);

                    if (existResource is not null)
                    {
                        // Если изменилось только количество ресурса.
                        if (resourceShipment.ResourceId == existResource.ResourceId &&
                            resourceShipment.MeasureUnitId == existResource.MeasureUnitId)
                        {
                            // Разница между новым и существующим количеством.
                            int diff = resourceShipment.Quantity - existResource.Quantity;

                            if (diff != 0)
                            {
                                BalanceEntity balance = GetExistingBalance(balanceDict,
                                    resourceShipment.ResourceId, resourceShipment.MeasureUnitId);

                                balance.Quantity -= diff;

                                if (balance.Quantity < 0)
                                {
                                    throw new InvalidOperationException(
                                        $"Недостаточно ресурса у товара: {resourceShipment.ResourceEntity!.Title}.");
                                }
                            }
                        }

                        else
                        {
                            // Возвращаем количество ресурса.
                            BalanceEntity oldBalance = GetExistingBalance(balanceDict, existResource.ResourceId,
                                existResource.MeasureUnitId);

                            oldBalance.Quantity += existResource.Quantity;

                            // Вычитаем новый ресурс с баланса.
                            BalanceEntity newBalance = GetExistingBalance(balanceDict, resourceShipment.ResourceId,
                                resourceShipment.MeasureUnitId);

                            newBalance.Quantity -= resourceShipment.Quantity;

                            if (newBalance.Quantity < 0)
                            {
                                throw new InvalidOperationException(
                                    $"Недостаточно добавленного ресурса Id: {resourceShipment.ResourceId}.");
                            }
                        }

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

                        BalanceEntity balance = GetExistingBalance(balanceDict, resourceShipment.ResourceId,
                            resourceShipment.MeasureUnitId);

                        balance.Quantity -= resourceShipment.Quantity;

                        if (balance.Quantity < 0)
                        {
                            throw new InvalidOperationException(
                                $"Недостаточно ресурса Id: {resourceShipment.ResourceId}.");
                        }
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
        public async Task RemoveDocumentShipmentAsync(int documentShipmentId)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                DocumentShipmentEntity? entity = await _db.DocumentShipments
                    .Include(ds => ds.ResourceShipmentEntities)
                    .FirstOrDefaultAsync(ds => ds.Id == documentShipmentId);

                if (entity is null)
                {
                    throw new InvalidOperationException("Документ отгрузк не найден. " +
                                                        $"DocumentShipmentId: {documentShipmentId} не найден.");
                }

                List<int> resourceIds = entity.ResourceShipmentEntities
                    .Select(rs => rs.ResourceId)
                    .Distinct()
                    .ToList();

                List<int> measureUnitIds = entity.ResourceShipmentEntities
                    .Select(rs => rs.MeasureUnitId)
                    .Distinct()
                    .ToList();

                Dictionary<(int, int), BalanceEntity> balanceDict = await _db.Balances
                    .Where(rr => resourceIds.Contains(rr.ResourceId) &&
                                 measureUnitIds.Contains(rr.MeasureUnitId))
                    .ToDictionaryAsync(rr => (rr.ResourceId, rr.MeasureUnitId), rr => rr);

                foreach (var item in entity.ResourceShipmentEntities!)
                {
                    BalanceEntity b = GetExistingBalance(balanceDict, item.ResourceId, item.MeasureUnitId);

                    // Возаращаем количество к балансу.
                    b.Quantity += item.Quantity;
                }

                await _db.ResourceShipments
                    .Where(rs => rs.DocumentShipmentId == documentShipmentId)
                    .ExecuteDeleteAsync();

                await _db.DocumentShipments
                    .Where(dr => dr.Id == documentShipmentId)
                    .ExecuteDeleteAsync();

                await _db.SaveChangesAsync();
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

        /// <summary>
        /// Метод получает баланс.
        /// </summary>
        /// <param name="dict">Словарь балансов.</param>
        /// <param name="resourceId">Id ресурса.</param>
        /// <param name="measureUnitId">Id единица измерения.</param>
        /// <returns>Найденный баланс.</returns>
        private BalanceEntity GetExistingBalance(Dictionary<(int, int), BalanceEntity> dict,
            int resourceId, int measureUnitId)
        {
            (int, int) key = (resourceId, measureUnitId);

            if (!dict.TryGetValue(key, out var balance))
            {
                throw new InvalidOperationException(
                    $"Ресурс Id: {resourceId} полностью отсутствует на балансе. Отгрузка невозможна.");
            }

            return balance!;
        }

        #endregion
    }
}
