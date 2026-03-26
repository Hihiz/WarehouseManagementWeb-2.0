using Microsoft.Extensions.Logging;
using Moq;
using WarehouseManagementWeb.Application.Interfaces.Repositories.DocumentReceipt;
using WarehouseManagementWeb.Application.Services.DocumentReceipt;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentReceipt
{
    /// <summary>
    /// Базовый класс юнит тестов сервиса документов поступлений.
    /// </summary>
    public class BaseDocumentReceiptTest
    {
        protected internal readonly Mock<IDocumentReceiptRepository> mockDocumentReceiptRepository;
        protected internal readonly Mock<ILogger<DocumentReceiptService>> mockLogger;

        protected internal readonly DocumentReceiptService documentReceiptService;

        protected internal BaseDocumentReceiptTest()
        {
            mockDocumentReceiptRepository = new Mock<IDocumentReceiptRepository>();
            mockLogger = new Mock<ILogger<DocumentReceiptService>>();

            documentReceiptService = new DocumentReceiptService(mockDocumentReceiptRepository.Object,
                mockLogger.Object);
        }
    }
}
