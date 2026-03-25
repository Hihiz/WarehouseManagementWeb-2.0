namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class RemoveClientTest : BaseIntegrationTest
    {
        public RemoveClientTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task RemoveClientAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            // Act
            await clientRepository.RemoveClientAsync(client.Id);

            // Assert
            var removeClient = await clientRepository.GetClientByIdAsync(client.Id);
            Assert.Null(removeClient);
        }

        [Fact]
        public async Task RemoveClientAsyncNotAffectedRowTest()
        {
            // Arrange
            int removedId = int.MaxValue;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await clientRepository.RemoveClientAsync(removedId));
        }
    }
}