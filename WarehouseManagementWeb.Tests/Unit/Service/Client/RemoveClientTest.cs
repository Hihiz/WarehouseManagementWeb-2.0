using Moq;
namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class RemoveClientTest : BaseClientServiceTest
    {
        [Fact]
        public async Task RemoveClientAsyncTest()
        {
            // Arrange
            int clientId = 1;

            //  Act & Assert
            await clientService.RemoveClientAsync(clientId);

            mockClientRepository.Verify(r => r.RemoveClientAsync(It.IsAny<int>()),
                Times.Once);
        }

        [Fact]
        public async Task RemoveClientAsyncNullTest()
        {
            // Arrange &  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                     async () => await clientService.RemoveClientAsync(0));

            mockClientRepository.Verify(r => r.RemoveClientAsync(It.IsAny<int>()),
             Times.Never);
        }
    }
}
