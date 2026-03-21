using System.Xml.Linq;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.Client
{
    public class CheckClientExistsByNameTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CheckClientExistsByNameIsExistsAsyncTest()
        {
            // Act
            var client = new ClientEntity
            {
                Name = "Test" + Guid.NewGuid().ToString()[..5],
                Address = "Тестовый адрес" + Guid.NewGuid().ToString()
            };
            await clientRepository.CreateClientAsync(client);

            var result = await clientRepository.CheckClientExistsByNameAsync(client.Name);

            Assert.True(result);
        }

        [Fact]
        public async Task CheckClientExistsByNameIsNotExistsAsyncTest()
        {
            string clientTitle = "Test" + Guid.NewGuid().ToString()[..5];

            var result = await clientRepository.CheckClientExistsByNameAsync(clientTitle);

            Assert.False(result);
        }
    }
}