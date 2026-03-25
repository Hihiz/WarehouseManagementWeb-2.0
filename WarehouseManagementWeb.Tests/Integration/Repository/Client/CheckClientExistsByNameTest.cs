namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CheckClientExistsByNameTest : BaseIntegrationTest
    {
        public CheckClientExistsByNameTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CheckClientExistsByNameIsExistsAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            // Act
            var result = await clientRepository.CheckClientExistsByNameAsync(client.Name);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckClientExistsByNameIsNotExistsAsyncTest()
        {
            // Arrange
            string clientName = faker.Company.CompanyName();

            // Act
            var result = await clientRepository.CheckClientExistsByNameAsync(clientName);

            // Assert
            Assert.False(result);
        }
    }
}