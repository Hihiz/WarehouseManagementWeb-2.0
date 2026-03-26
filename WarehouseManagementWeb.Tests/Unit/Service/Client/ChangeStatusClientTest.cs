using WarehouseManagementWeb.Application.Dto.Input.Client;

namespace WarehouseManagementWeb.Tests.Unit.Service.Client
{
    public class ChangeStatusClientTest : BaseClientServiceTest
    {
        [Fact]
        public async Task ChangeStatusClientAsyncTest()
        {
            // Arrange
            var input = new ChangeStatusClientInput
            {
                ClientId = 1,
                ClientStatusEnum = Domain.Enums.DirectoryStatusEnum.Archived
            };

            //  Act & Assert
            await clientService.ChangeStatusClientAsync(input);
        }

        [Fact]
        public async Task ChangeStatusClientAsyncNullTest()
        {
            // Arrange
            ChangeStatusClientInput input = null;

            //  Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                     async () => await clientService.ChangeStatusClientAsync(input));
        }
    }
}
