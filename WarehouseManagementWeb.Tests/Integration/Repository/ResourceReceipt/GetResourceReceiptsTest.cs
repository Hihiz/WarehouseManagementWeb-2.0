namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class GetResourceReceiptsTest : BaseIntegrationTest
    {

        public GetResourceReceiptsTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetResourceReceiptsAsyncTest()
        {
            var result = await resourceReceiptRepository.GetResourceReceiptsAsync();

            Assert.NotNull(result);
        }
    }
}
