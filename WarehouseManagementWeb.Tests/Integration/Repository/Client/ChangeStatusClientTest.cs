using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class ChangeStatusClientTest : BaseIntegrationTest
    {
        public ChangeStatusClientTest(DatabaseFixture fixture) : base(fixture) { }


        [Fact]
        public async Task ChangeStatusClientAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();
            var clientId = client.Id;
            var updatedStatus = DirectoryStatusEnum.Active;

            // Act
            await clientRepository.ChangeStatusClientAsync(clientId, updatedStatus);

            // Assert
            var actualClient = await clientRepository.GetClientByIdAsync(clientId);
            Assert.NotNull(actualClient);
            Assert.Equal(updatedStatus, actualClient.ClientStatusEnum);
        }

        [Fact]
        public async Task ChangeStatusClientNotFoundTest()
        {
            // Arrange
            var notExistId = int.MaxValue;
            var updatedStatus = DirectoryStatusEnum.Archived;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await clientRepository.ChangeStatusClientAsync(notExistId, updatedStatus));
        }
    }
}
