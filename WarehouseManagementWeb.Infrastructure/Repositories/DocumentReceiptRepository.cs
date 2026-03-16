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
                .OrderByDescending(dr => dr.Id)
                .Select(x => new ResourceReceiptListOutput
                {
                    DocumentReceiptId = x.Id,
                    DocumentReceiptNumberCode = x.NumberCode,
                    DocumentReceiptDate = x.Date,
                    DocumentClientName = x.ClientEntity!.Name,
                    Items = x.ResourceReceiptEntities!
                    .OrderByDescending(rr => rr.Id)
                    .Select(rr => new ResourceReceiptItemOutput
                    {
                        ResourceReceiptId = rr.Id,
                        ResourceTitle = rr.ResourceEntity!.Title,
                        MeasureUnitTitle = rr.MeasureUnitEntity!.Title,
                        ResourceQuantity = rr.Quantity
                    })

                })
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<ResourceReceiptListOutput> GetResourceReceiptByDocumentReceiptIdAsync(
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
                      DocumentClientName = dr.ClientEntity!.Name,
                      Items = dr.ResourceReceiptEntities!
                      .OrderByDescending(rr => rr.Id)
                      .Select(rr => new ResourceReceiptItemOutput
                      {
                          ResourceReceiptId = rr.Id,
                          ResourceTitle = rr.ResourceEntity!.Title,
                          MeasureUnitTitle = rr.MeasureUnitEntity!.Title,
                          ResourceQuantity = rr.Quantity
                      })
                  }).FirstOrDefaultAsync();

            return result!;
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
            await _db.DocumentReceipts.AddAsync(documentEntity);

            await _db.SaveChangesAsync();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}