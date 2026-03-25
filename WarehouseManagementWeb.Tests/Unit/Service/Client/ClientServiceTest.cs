using Microsoft.Extensions.Logging;
using Moq;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Application.Interfaces.Repositories.Client;
using WarehouseManagementWeb.Application.Services.Client;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    /// <summary>
    /// Класс unit теста сервиса клиентов.
    /// </summary>
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<ILogger<ClientService>> _mockLogger;

        private readonly ClientService _clientService;

        public ClientServiceTest()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockLogger = new Mock<ILogger<ClientService>>();

            _clientService = new ClientService(_mockClientRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetClientsAsyncTest()
        {
            // Arrange
            var mockClients = new List<ClientOutput>
            {
                new ClientOutput { Id = 1, ClientStatusEnum = DirectoryStatusEnum.Active },
                new ClientOutput { Id = 2, ClientStatusEnum = DirectoryStatusEnum.Archived },
                new ClientOutput { Id = 3, ClientStatusEnum = DirectoryStatusEnum.Active },
                new ClientOutput { Id = 4, ClientStatusEnum = DirectoryStatusEnum.Archived }
            };

            _mockClientRepository
                .Setup(repo => repo.GetClientsAsync())
                .ReturnsAsync(mockClients);

            // Act
            var result = await _clientService.GetClientsAsync();

            // Assert
            Assert.NotNull(result);

            Assert.Equal(2, result.ActiveClients.Count());
            Assert.Equal(2, result.ArchivedClients.Count());
           
            Assert.Equal(3, result.ActiveClients.First().Id);

            Assert.Equal(4, result.ArchivedClients.First().Id);
        }
    }
}
