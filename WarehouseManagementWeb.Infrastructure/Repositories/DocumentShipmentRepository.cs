using Microsoft.EntityFrameworkCore;
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

        public Task<IEnumerable<ResourceShipmentListOutput>> GetResourceShipmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResourceShipmentListOutput> GetResourceShipmentByDocumentShipmentIdAsync(int documentShipmentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDocumentShipmentExistsByNumberCodeAsync(string documentShipmentNumberCode)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<bool> CheckDocumentShipmentExistsByIdAndNumberCodeAsync(int documentShipmentId, string documentShipmentNumberCode)
        {
            throw new NotImplementedException();
        }

        public Task CreateResourceShipmentAsync(DocumentShipmentEntity documentEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceShipmentAsync(DocumentShipmentEntity documentEntity)
        {
            throw new NotImplementedException();
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

        public Task RemoveDocumentShipmentAsync(int documentShipmentId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
