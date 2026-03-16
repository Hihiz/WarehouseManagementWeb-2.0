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
        #endregion

        #region Приватные методы.

        #endregion
    }
}