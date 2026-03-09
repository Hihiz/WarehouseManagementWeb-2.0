namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetActiveClientsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetActiveClientsAsyncTest()
        {
            var result = await clientRepository.GetActiveClientsAsync();

            Assert.NotNull(result);
        }
    }
}
