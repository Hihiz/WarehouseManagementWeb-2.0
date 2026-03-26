using Moq;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class GetClientsTest : BaseClientServiceTest
    {
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

            mockClientRepository
                .Setup(repo => repo.GetClientsAsync())
                .ReturnsAsync(mockClients);

            // Act
            var result = await clientService.GetClientsAsync();

            // Assert
            Assert.NotNull(result);

            Assert.Equal(2, result.ActiveClients.Count());
            Assert.Equal(2, result.ArchivedClients.Count());

            Assert.Equal(3, result.ActiveClients.First().Id);
            Assert.Equal(4, result.ArchivedClients.First().Id);
        }

        [Fact]
        public async Task GetClientsAsyncNullTest()
        {
            // Arrange
            List<ClientOutput> clients = null;

            mockClientRepository
                .Setup(repo => repo.GetClientsAsync())
                .ReturnsAsync(clients);

            // Act
            var result = await clientService.GetClientsAsync();

            // Assert
            Assert.NotNull(result);

            Assert.Null(result.ActiveClients);
            Assert.Null(result.ArchivedClients);
        }

        [Fact]
        public async Task GetClientsAsyncEmptyTest()
        {
            // Arrange
            mockClientRepository
                .Setup(repo => repo.GetClientsAsync())
                .ReturnsAsync(new List<ClientOutput>());

            // Act
            var result = await clientService.GetClientsAsync();

            // Assert
            Assert.NotNull(result);

            Assert.Null(result.ActiveClients);
            Assert.Null(result.ArchivedClients);
        }

        [Fact]
        public async Task GetClientsAsyncThrowExceptionTest()
        {
            // Arrange
            var exception = new Exception("Ошибка");

            mockClientRepository
                .Setup(repo => repo.GetClientsAsync())
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(clientService.GetClientsAsync);
        }
    }
}
