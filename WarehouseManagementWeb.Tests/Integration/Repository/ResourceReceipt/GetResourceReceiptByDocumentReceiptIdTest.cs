namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class GetResourceReceiptByDocumentReceiptIdTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetResourceReceiptByDocumentReceiptIdAsyncTest()
        {
            var result = await resourceReceiptRepository.GetResourceReceiptByDocumentReceiptIdAsync(16);

            Assert.NotNull(result);
        }
    }
}
