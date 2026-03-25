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
            var client = await SeedClientAsync();

            var updatedName = Guid.NewGuid().ToString();
            var updatedAddress = faker.Address.FullAddress();

            var updatedClient = new ClientEntity
            {
                Id = client.Id,
                Name = updatedName,
                Address = updatedAddress,
            };

            // Act
            await clientRepository.UpdateClientAsync(updatedClient);

           // Assert
             var actualClient = await clientRepository.GetClientByIdAsync(client.Id);
            Assert.NotNull(actualClient);
            Assert.Equal(updatedName, actualClient.Name);
            Assert.Equal(updatedAddress, actualClient.Address);
        }

        [Fact]
        public async Task UpdateClientAsyncNotFoundTest()
        {
            // Arrange
            var notExistId = int.MaxValue;
            var clientName = faker.Company.CompanyName();
            var clientAddress = faker.Address.FullAddress();

            var client = new ClientEntity
            {
                Id = notExistId,
                Name = clientName,
                Address = clientAddress,
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await clientRepository.UpdateClientAsync(client));
        }

        [Fact]
        public async Task UpdateClientAsyncDuplicateNameTest()
        {
            // Arrange
            var client1 = await SeedClientAsync();
            var client2 = await SeedClientAsync();

            var updatedClient1 = new ClientEntity
            {
                Id = client1.Id,
                Name = client2.Name,
                Address = faker.Address.FullAddress(),
            };

            // Act & Assert          
            await Assert.ThrowsAsync<DbUpdateException>(
                async () => await clientRepository.UpdateClientAsync(updatedClient1));
        }
    }
}
