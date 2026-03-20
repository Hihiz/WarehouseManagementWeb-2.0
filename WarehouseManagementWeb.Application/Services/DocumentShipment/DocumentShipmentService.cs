using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Application.Dto.Output.ResourceShipment;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentShipment;

namespace WarehouseManagementWeb.Application.Services.DocumentShipment
{
    /// <summary>
    /// Класс реализует методы документа отгрузки.
    /// </summary>
    public class DocumentShipmentService : IDocumentShipmentService
    {
        private readonly ILogger<DocumentShipmentService> _logger;
        private readonly IDocumentReceiptRepository _documentShipmentRepository;

        public Task ChangeStatusDocumentShipmentAsync(ChangeStatusDocumentShipmentInput input)
        {
            throw new NotImplementedException();
        }

        public Task CreateResourceShipmentAsync(CreateResourceShipmentInput input)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceShipmentListOutput?> GetResourceShipmentByDocumentShipmentIdAsync(int documentShipmentId)
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

        public Task UpdateResourceShipmentAsync(UpdateResourceShipmentInput input)
        {
            throw new NotImplementedException();
        }
    }
}
