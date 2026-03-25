using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CreateClientTest : BaseIntegrationTest
    {
        public CreateClientTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CreateClientAsyncTest()
        {
            // Arrange
            var client = new ClientEntity
            {
                Name = faker.Company.CompanyName(),
                Address = faker.Address.FullAddress(),
            };

            // Act & Assert
            await clientRepository.CreateClientAsync(client);

            // Проверка, что клиент создан
            var actualClient = await clientRepository.GetClientByIdAsync(client.Id);
            Assert.NotNull(actualClient);
            Assert.Equal(client.Name, actualClient.Name);
            Assert.Equal(client.Address, actualClient.Address);
        }

        [Fact]
        public async Task CreateClientAsyncDuplicateNameTest()
        {
            // Arrange
            var existingClient = await SeedClientAsync();

            var duplicateClient = new ClientEntity
            {
                Name = existingClient.Name,
                Address = faker.Address.FullAddress(),
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(
                async () => await clientRepository.CreateClientAsync(duplicateClient));
        }
    }
}