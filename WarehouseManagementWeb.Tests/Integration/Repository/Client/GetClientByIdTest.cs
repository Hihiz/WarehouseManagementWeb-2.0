using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetClientByIdTest : BaseIntegrationTest
    {
        public GetClientByIdTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetClientByIdAsyncTest()
        {
            // Act
            var client = new ClientEntity
            {
                Name = "Test" + Guid.NewGuid().ToString(),
                Address = "Тестовый адрес" + Guid.NewGuid().ToString()
            };
            await clientRepository.CreateClientAsync(client);

            // Arrange & Assert
            var result = await clientRepository.GetClientByIdAsync(client.Id);

            Assert.NotNull(result);
        }
    }
}
