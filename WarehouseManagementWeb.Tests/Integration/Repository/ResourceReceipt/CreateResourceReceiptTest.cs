using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class CreateResourceReceiptTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CreateResourceReceiptAsyncTest()
        {
            // Act
            var client = new ClientEntity { Name = "Test", Address = "Тестовый адрес" };
            await clientRepository.CreateClientAsync(client);

            var mu1 = new MeasureUnitEntity { Title = "Кирпич М100" };
            var mu2 = new MeasureUnitEntity { Title = "Цемент М500" };
            await measureUnitRepository.CreateMeasureUnitAsync(mu1);
            await measureUnitRepository.CreateMeasureUnitAsync(mu2);

            var resource1 = new ResourceEntity { Title = "Кирпич М100" };
            var resource2 = new ResourceEntity { Title = "Цемент М500" };
            await resourceRepository.CreateResourceAsync(resource1);
            await resourceRepository.CreateResourceAsync(resource2);

            var document = new DocumentReceiptEntity
            {
                NumberCode = "NumberCode",
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            document.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource2.Id,
                MeasureUnitId = mu2.Id,
                Quantity = 850
            });
            document.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = mu1.Id,
                Quantity = 120
            });

            // Arrange & Assert
            await resourceReceiptRepository.CreateResourceReceiptAsync(document);
        }
    }
}
