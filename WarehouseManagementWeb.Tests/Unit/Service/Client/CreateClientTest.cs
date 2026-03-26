using Moq;
using WarehouseManagementWeb.Application.Dto.Input.Client;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class CreateClientTest : BaseClientServiceTest
    {
        [Fact]
        public async Task CreateClientAsyncTest()
        {
            // Arrange
            var input = new CreateClientInput { Name = "Test Client" };

            mockClientRepository
                .Setup(repo => repo.CheckClientExistsByNameAsync(input.Name))
                .ReturnsAsync(false);

            //  Act & Assert
            await clientService.CreateClientAsync(input);
        }

        [Fact]
        public async Task CreateClientAsyncNameDuplicateTest()
        {
            // Arrange
            var input = new CreateClientInput { Name = "Test Client" };

            mockClientRepository
                .Setup(repo => repo.CheckClientExistsByNameAsync(input.Name))
                .ReturnsAsync(true);

            //  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                       async () => await clientService.CreateClientAsync(input));

            mockClientRepository.Verify(r => r.CreateClientAsync(It.IsAny<ClientEntity>()),
                Times.Never);
        }

        [Fact]
        public async Task CreateClientAsyncNullTest()
        {
            // Arrange
            CreateClientInput input = null;

            //  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                     async () => await clientService.CreateClientAsync(input));
        }
    }
}
