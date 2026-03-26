using Moq;
using WarehouseManagementWeb.Application.Dto.Output.Client;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class GetClientByIdTest : BaseClientServiceTest
    {
        [Fact]
        public async Task GetClientByIdAsyncTest()
        {
            // Arrange
            var client = new ClientOutput
            {
                Id = 1,
                Name = "Test Client",
                ClientStatusEnum = Domain.Enums.DirectoryStatusEnum.Active
            };

            mockClientRepository
                .Setup(repo => repo.GetClientByIdAsync(client.Id))
                .ReturnsAsync(client);

            //  Act
            var result = await clientService.GetClientByIdAsync(client.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.Id, result.Id);
            Assert.True(client.ClientStatusEnum is Domain.Enums.DirectoryStatusEnum.Active);

            mockClientRepository.Verify(r => r.GetClientByIdAsync(It.IsAny<int>()),
               Times.Once);
        }

        [Fact]
        public async Task GetClientByIdAsyncThrowExceptionTest()
        {
            // Arrange
            var clientId = 0;

            //  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await clientService.GetClientByIdAsync(clientId));

            mockClientRepository.Verify(r => r.GetClientByIdAsync(It.IsAny<int>()),
                Times.Never);
        }

        [Fact]
        public async Task GetClientByIdAsyncNullTest()
        {
            // Arrange
            var clientId = 1;

            ClientOutput client = null;

            mockClientRepository
                .Setup(repo => repo.GetClientByIdAsync(clientId))
                .ReturnsAsync(client);

            //  Act
            var result = await clientService.GetClientByIdAsync(clientId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id == 0);


            mockClientRepository.Verify(r => r.GetClientByIdAsync(It.IsAny<int>()),
                Times.Once);
        }
    }
}
