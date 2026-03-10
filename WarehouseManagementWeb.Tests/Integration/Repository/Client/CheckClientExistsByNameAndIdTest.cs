using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CheckClientExistsByNameAndIdTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CheckClientExistsByNameAndIdAsyncTest()
        {
            // Arrange
            var name1 = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            var client1 = new ClientEntity
            {
                Name = name1,
                Address = "Test Address1"
            };
            await clientRepository.CreateClientAsync(client1);

            var client2 = new ClientEntity
            {
                Name = name2,
                Address = "Test Address2"
            };
            await clientRepository.CreateClientAsync(client2);

            // Act
            var updatedName = "updated name";
            var result = await clientRepository.CheckClientExistsByNameAndIdAsync(client2.Id, updatedName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckClientExistsByNameAndIdDuplicateNameAsyncTest()
        {
            // Arrange
            var name1 = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            var client1 = new ClientEntity
            {
                Name = name1,
                Address = "Test Address1"
            };
            await clientRepository.CreateClientAsync(client1);

            var client2 = new ClientEntity
            {
                Name = name2,
                Address = "Test Address2"
            };
            await clientRepository.CreateClientAsync(client2);

            // Act
            bool result = await clientRepository.CheckClientExistsByNameAndIdAsync(client2.Id, name1);

            // Assert
            Assert.True(result);
        }
    }
}
