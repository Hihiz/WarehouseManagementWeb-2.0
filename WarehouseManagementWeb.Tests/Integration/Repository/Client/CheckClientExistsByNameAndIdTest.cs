namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CheckClientExistsByNameAndIdTest : BaseIntegrationTest
    {
        public CheckClientExistsByNameAndIdTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CheckClientExistsByNameAndIdAsyncTest()
        {
            // Arrange
            var client1 = await SeedClientAsync();
            var client2 = await SeedClientAsync();

            // Act
            var updatedName = faker.Company.CompanyName();
            var result = await clientRepository.CheckClientExistsByNameAndIdAsync(client2.Id, updatedName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckClientExistsByNameAndIdDuplicateNameAsyncTest()
        {
            // Arrange
            var client1 = await SeedClientAsync();
            var client2 = await SeedClientAsync();

            client2.Name = client1.Name;

            // Act
            bool result = await clientRepository.CheckClientExistsByNameAndIdAsync(client2.Id, client2.Name);

            // Assert
            Assert.True(result);
        }
    }
}
