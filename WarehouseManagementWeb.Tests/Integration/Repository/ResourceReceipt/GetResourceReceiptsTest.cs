namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class GetResourceReceiptsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetResourceReceiptsAsyncTest()
        {
            var result = await resourceReceiptRepository.GetResourceReceiptsAsync();

            Assert.NotNull(result);
        }
    }
}
