using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CreateClientTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CreateClientAsyncTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();

            var client = new ClientEntity
            {
                Name = name,
                Address = "Test Address",
            };

            // Act & Assert
            await clientRepository.CreateClientAsync(client);
        }

        [Fact]
        public async Task CreateClientAsyncDuplicateNameTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();

            var client1 = new ClientEntity
            {
                Name = name,
                Address = "Test Address1",
            };

            await clientRepository.CreateClientAsync(client1);

            var client2 = new ClientEntity
            {
                Name = name,
                Address = "Test Address2",
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(
                    async () => await clientRepository.CreateClientAsync(client2));
        }
    }
}