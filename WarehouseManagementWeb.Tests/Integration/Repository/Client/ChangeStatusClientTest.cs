using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class ChangeStatusClientTest : BaseIntegrationTest
    {
        [Fact]
        public async Task ChangeStatusClientAsyncTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();

            var client = new ClientEntity
            {
                Name = name,
                Address = "Test Address",
                ClientStatusEnum = DirectoryStatusEnum.Archived
            };

            await clientRepository.CreateClientAsync(client);

            var clientId = client.Id;
            var updatedStatus = DirectoryStatusEnum.Active;

            // Act & Assert
            await clientRepository.ChangeStatusClientAsync(clientId, updatedStatus);
        }

        [Fact]
        public async Task ChangeStatusClientNotFoundTest()
        {
            // Arrange
            var notExistId = Int32.MaxValue;
            var updatedStatus = DirectoryStatusEnum.Archived;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await clientRepository.ChangeStatusClientAsync(notExistId, updatedStatus));
        }
    }
}
