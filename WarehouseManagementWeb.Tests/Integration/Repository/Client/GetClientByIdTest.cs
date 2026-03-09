namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetClientByIdTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetClientByIdAsyncTest()
        {
            var result = await clientRepository.GetClientByIdAsync(1);

            Assert.NotNull(result);
        }
    }
}
