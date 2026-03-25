namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetClientsTest : BaseIntegrationTest
    {
        public GetClientsTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetClientsAsyncTest()
        {
            var result = await clientRepository.GetClientsAsync();

            Assert.NotNull(result);
        }
    }
}
