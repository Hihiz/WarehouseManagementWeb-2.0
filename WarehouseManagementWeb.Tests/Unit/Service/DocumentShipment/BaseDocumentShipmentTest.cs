
using Microsoft.Extensions.Logging;
using Moq;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentShipment;
using WarehouseManagementWeb.Application.Services.DocumentShipment;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentShipment
{
    /// <summary>
    /// Базовый класс юнит тестов сервис дкументов отгрузок.
    /// </summary>
    public class BaseDocumentShipmentTest
    {
        protected internal readonly Mock<IDocumentShipmentRepository> mockDocumentShipmentRepository;
        protected internal readonly Mock<ILogger<DocumentShipmentService>> mockLogger;

        protected internal readonly DocumentShipmentService documentShipmentService;

        protected internal BaseDocumentShipmentTest()
        {
            mockDocumentShipmentRepository = new Mock<IDocumentShipmentRepository>();
            mockLogger = new Mock<ILogger<DocumentShipmentService>>();

            documentShipmentService = new DocumentShipmentService(mockDocumentShipmentRepository.Object,
                mockLogger.Object);
        }
    }
}
