
using Moq;
using WarehouseManagementWeb.Application.Dto.Input.Client;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class UpdateClientTest : BaseClientServiceTest
    {
        [Fact]
        public async Task UpdateClientAsyncTest()
        {
            // Arrange
            var input = new UpdateClientInput { Id = 1, Name = "Test Client" };

            mockClientRepository
                .Setup(repo => repo.CheckClientExistsByNameAndIdAsync(input.Id, input.Name))
                .ReturnsAsync(false);

            //  Act & Assert
            await clientService.UpdateClientAsync(input);

            mockClientRepository.Verify(r => r.UpdateClientAsync(It.IsAny<ClientEntity>()),
               Times.Once);
        }

        [Fact]
        public async Task UpdateClientAsyncNameDuplicateTest()
        {
            // Arrange
            var input = new UpdateClientInput { Id = 1, Name = "Test Client" };

            mockClientRepository
                 .Setup(repo => repo.CheckClientExistsByNameAndIdAsync(input.Id, input.Name))
                 .ReturnsAsync(true);


            //  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                       async () => await clientService.UpdateClientAsync(input));

            mockClientRepository.Verify(r => r.UpdateClientAsync(It.IsAny<ClientEntity>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateClientAsyncNullTest()
        {
            // Arrange
            UpdateClientInput input = null;

            //  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                     async () => await clientService.UpdateClientAsync(input));

            mockClientRepository.Verify(r => r.UpdateClientAsync(It.IsAny<ClientEntity>()),
             Times.Never);
        }
    }
}
