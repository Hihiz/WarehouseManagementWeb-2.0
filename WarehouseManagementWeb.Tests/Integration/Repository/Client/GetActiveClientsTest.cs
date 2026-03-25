namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetActiveClientsTest : BaseIntegrationTest
    {
        public GetActiveClientsTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetActiveClientsAsyncTest()
        {
            var result = await clientRepository.GetActiveClientsAsync();

            Assert.NotNull(result);
        }
    }
}
