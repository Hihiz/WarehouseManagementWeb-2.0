namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetClientsTest : BaseIntegrationTest
    {
        public GetClientsTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetClientsAsyncTest()
        {
            // Arrange
            var client1 = await SeedClientAsync();
            var client2 = await SeedClientAsync();

            // Act
            var result = await clientRepository.GetClientsAsync();

            // Arrange
            Assert.NotNull(result);
        }
    }
}
