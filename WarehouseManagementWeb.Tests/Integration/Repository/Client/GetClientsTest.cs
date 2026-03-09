namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetClientsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetClientsAsyncTest()
        {
            var result = await clientRepository.GetClientsAsync();

            Assert.NotNull(result);
        }
    }
}
