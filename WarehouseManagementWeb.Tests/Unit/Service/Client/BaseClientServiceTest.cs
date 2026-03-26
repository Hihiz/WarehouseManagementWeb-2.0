using Microsoft.Extensions.Logging;
using Moq;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
using WarehouseManagementWeb.Application.Services.Client;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    /// <summary>
    /// Базовый класс юнит тестов сервиса клентов.
    /// </summary>
    public class BaseClientServiceTest
    {
        protected internal readonly Mock<IClientRepository> mockClientRepository;
        protected internal readonly Mock<ILogger<ClientService>> mockLogger;

        protected internal readonly ClientService clientService;

        protected internal BaseClientServiceTest()
        {
            mockClientRepository = new Mock<IClientRepository>();
            mockLogger = new Mock<ILogger<ClientService>>();

            clientService = new ClientService(mockClientRepository.Object, mockLogger.Object);
        }
    }
}
