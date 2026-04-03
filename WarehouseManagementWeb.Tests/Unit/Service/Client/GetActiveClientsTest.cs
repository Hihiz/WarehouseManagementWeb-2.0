using Moq;
using WarehouseManagementWeb.Application.Dto.Output.Client;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class GetActiveClientsTest : BaseClientServiceTest
    {
        [Fact]
        public async Task GetActiveClientsAsync()
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
                .Setup(repo => repo.GetActiveClientsAsync(null))
                .ReturnsAsync(mockClients);

            // Act
            var result = await clientService.GetActiveClientsAsync(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count(c => c.ClientStatusEnum == DirectoryStatusEnum.Active));
        }

        [Fact]
        public async Task GetActiveClientsAsyncNullTest()
        {
            // Arrange
            List<ClientOutput> clients = null;

            mockClientRepository
                .Setup(repo => repo.GetActiveClientsAsync(null))
                .ReturnsAsync(clients);

            // Act
            var result = await clientService.GetActiveClientsAsync(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetActiveClientsAsyncEmptyTest()
        {
            // Arrange
            mockClientRepository
                .Setup(repo => repo.GetActiveClientsAsync(null))
                .ReturnsAsync(new List<ClientOutput>());

            // Act
            var result = await clientService.GetActiveClientsAsync(null);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetActiveClientsAsyncThrowExceptionTest()
        {
            // Arrange
            var exception = new Exception("Ошибка");

            mockClientRepository
                .Setup(repo => repo.GetActiveClientsAsync(null))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => clientService.GetActiveClientsAsync(null));
        }
    }
}