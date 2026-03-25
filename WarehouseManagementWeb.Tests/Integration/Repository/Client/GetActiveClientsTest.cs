namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetActiveClientsTest : BaseIntegrationTest
    {
        public GetActiveClientsTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetActiveClientsAsyncTest()
        {
            // Arrange
            var activeClient1 = await SeedClientAsync();
            var activeClient2 = await SeedClientAsync();
          
            // Act
            var result = await clientRepository.GetActiveClientsAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
