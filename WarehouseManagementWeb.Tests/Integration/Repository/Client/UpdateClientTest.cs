using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class UpdateClientTest : BaseIntegrationTest
    {
        public UpdateClientTest(DatabaseFixture fixture) : base(fixture) { }


        [Fact]
        public async Task UpdateClientAsyncTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();

            var client = new ClientEntity
            {
                Name = name,
                Address = "Test Address",
            };

            await clientRepository.CreateClientAsync(client);

            var updatedName = Guid.NewGuid().ToString();

            var updatedClient = new ClientEntity
            {
                Id = client.Id,
                Name = updatedName,
                Address = "New Address",
            };

            // Act & Assert
            await clientRepository.UpdateClientAsync(updatedClient);
        }

        [Fact]
        public async Task UpdateClientAsyncNotFoundTest()
        {
            // Arrange
            var notExistId = Int32.MaxValue;

            var client = new ClientEntity
            {
                Id = notExistId,
                Name = "Test Name",
                Address = "Test Address",
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await clientRepository.UpdateClientAsync(client));
        }

        [Fact]
        public async Task UpdateClientAsyncDuplicateNameTest()
        {
            // Arrange
            var name1 = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            var client1 = new ClientEntity
            {
                Name = name1,
                Address = "Test Address1",
            };
            await clientRepository.CreateClientAsync(client1);

            var client2 = new ClientEntity
            {
                Name = name2,
                Address = "Test Address2",
            };
            await clientRepository.CreateClientAsync(client2);

            var updatedClient1 = new ClientEntity
            {
                Id = client1.Id,
                Name = name2,
                Address = "Updated Address1",
            };

            // Act & Assert          
            await Assert.ThrowsAsync<DbUpdateException>(
                    async () => await clientRepository.UpdateClientAsync(updatedClient1));
        }
    }
}
