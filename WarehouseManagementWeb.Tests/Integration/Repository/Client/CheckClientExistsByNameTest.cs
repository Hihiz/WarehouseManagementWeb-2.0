namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CheckClientExistsByNameTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CheckClientExistsByNameIsExistsAsyncTest()
        {
            var result = await clientRepository.CheckClientExistsByNameAsync("test");

            Assert.True(result);
        }

        [Fact]
        public async Task CheckClientExistsByNameIsNotExistsAsyncTest()
        {
            var result = await clientRepository.CheckClientExistsByNameAsync("test999");

            Assert.False(result);
        }
    }
}
