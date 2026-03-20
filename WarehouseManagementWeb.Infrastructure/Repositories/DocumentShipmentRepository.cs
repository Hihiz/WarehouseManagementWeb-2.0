using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentShipment;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория документов отгрузок.
    /// </summary>
    public class DocumentShipmentRepository : IDocumentShipmentRepository
    {
        public Task ChangeStatusDocumentShipmentAsync(int documentShipmentId, DocumentStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDocumentShipmentExistsByIdAndNumberCodeAsync(int documentShipmentId, string documentShipmentNumberCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDocumentShipmentExistsByNumberCodeAsync(string documentShipmentNumberCode)
        {
            throw new NotImplementedException();
        }

        public Task CreateResourceShipmentAsync(DocumentShipmentEntity documentEntity)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceShipmentListOutput> GetResourceShipmentByDocumentShipmentIdAsync(int documentShipmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceShipmentListOutput>> GetResourceShipmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDocumentShipmentAsync(int documentShipmentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResourceShipmentAsync(DocumentShipmentEntity documentEntity)
        {
            throw new NotImplementedException();
        }
    }
}
