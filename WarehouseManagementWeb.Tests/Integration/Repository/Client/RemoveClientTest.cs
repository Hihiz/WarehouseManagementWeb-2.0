using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class RemoveClientTest : BaseIntegrationTest
    {
        [Fact]
        public async Task RemoveClientAsyncTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();

            var client = new ClientEntity
            {
                Name = name,
                Address = "Test Address",
            };

            await clientRepository.CreateClientAsync(client);

            var removedId = client.Id;

            // Act & Assert
            await clientRepository.RemoveClientAsync(removedId);
        }

        [Fact]
        public async Task RemoveClientAsyncNotAffectedRowTest()
        {
            // Arrange
            int removedId = int.MaxValue;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await clientRepository.RemoveClientAsync(removedId));
        }
    }
}