namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class GetClientByIdTest : BaseIntegrationTest
    {
        public GetClientByIdTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetClientByIdAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            // Act
            var result = await clientRepository.GetClientByIdAsync(client.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientByIdAsyncNullTest()
        {
            // Arrange
            var notFoundClient = int.MaxValue;

            // Act
            var result = await clientRepository.GetClientByIdAsync(notFoundClient);

            // Assert
            Assert.Null(result);
        }
    }
}
