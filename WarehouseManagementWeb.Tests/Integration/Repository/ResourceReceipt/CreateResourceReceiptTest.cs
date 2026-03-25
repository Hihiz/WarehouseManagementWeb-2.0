using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class CreateResourceReceiptTest : BaseIntegrationTest
    {
        public CreateResourceReceiptTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CreateResourceReceiptAsyncTest()
        {
            // Arrange
            var client = new ClientEntity { Name = "Test" + Guid.NewGuid().ToString(), 
                Address = "Тестовый адрес" + Guid.NewGuid().ToString() };
            await clientRepository.CreateClientAsync(client);

            var mu1 = new MeasureUnitEntity { Title = "Кирпич М100" + Guid.NewGuid().ToString() };
            var mu2 = new MeasureUnitEntity { Title = "Цемент М500" + Guid.NewGuid().ToString() };
            await measureUnitRepository.CreateMeasureUnitAsync(mu1);
            await measureUnitRepository.CreateMeasureUnitAsync(mu2);

            var resource1 = new ResourceEntity { Title = "Кирпич М100" + Guid.NewGuid().ToString() };
            var resource2 = new ResourceEntity { Title = "Цемент М500" + Guid.NewGuid().ToString() };
            await resourceRepository.CreateResourceAsync(resource1);
            await resourceRepository.CreateResourceAsync(resource2);

            var document = new DocumentReceiptEntity
            {
                NumberCode = "NumberCode" + Guid.NewGuid().ToString(),
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

            // Act & Assert.
            await resourceReceiptRepository.CreateResourceReceiptAsync(document);
        }
    }
}
