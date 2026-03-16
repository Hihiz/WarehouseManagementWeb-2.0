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

        public Task<bool> CheckDocumentReceiptExistsByIdAndNumberCodeAsync(int documentReceiptId, string documentReceiptNumberCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDocumentReceiptExistsByNumberCodeAsync(string documentReceiptNumberCode)
        {
            throw new NotImplementedException();
        }

        public Task CreateResourceReceiptAsync(DocumentReceiptEntity documentEntity)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceReceiptListOutput> GetResourceReceiptByDocumentReceiptIdAsync(int documentReceiptId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceReceiptListOutput>> GetResourceReceiptsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDocumentReceiptAsync(int documentReceiptId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceReceiptAsync(DocumentReceiptEntity documentEntity)
        {
            throw new NotImplementedException();
        }
    }
}