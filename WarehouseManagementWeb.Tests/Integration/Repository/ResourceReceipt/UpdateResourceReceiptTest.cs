using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class UpdateResourceReceiptTest : BaseIntegrationTest
    {
        [Fact]
        public async Task UpdateResourceReceiptAsyncTest()
        {
            // Arrange
            var client = new ClientEntity
            {
                Name = "Клиент для обновления",
                Address = "Тестовый адрес"
            };
            await clientRepository.CreateClientAsync(client);

            var resource1 = new ResourceEntity { Title = "Кирпич М100" };
            var resource2 = new ResourceEntity { Title = "Цемент М500" };
            var resource3 = new ResourceEntity { Title = "Арматура 12мм" };

            await resourceRepository.CreateResourceAsync(resource1);
            await resourceRepository.CreateResourceAsync(resource2);
            await resourceRepository.CreateResourceAsync(resource3);

            var muPiece = new MeasureUnitEntity { Title = "шт" };
            var muKg = new MeasureUnitEntity { Title = "кг" };

            await measureUnitRepository.CreateMeasureUnitAsync(muPiece);
            await measureUnitRepository.CreateMeasureUnitAsync(muKg);

            var originalDocument = new DocumentReceiptEntity
            {
                NumberCode = "NumberCode",
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            originalDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = muPiece.Id,
                Quantity = 100
            });

            originalDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource2.Id,
                MeasureUnitId = muKg.Id,
                Quantity = 500
            });

            await resourceReceiptRepository.CreateResourceReceiptAsync(originalDocument);

            // Act
            var updateDocument = new DocumentReceiptEntity
            {
                Id = originalDocument.Id,
                NumberCode = "UPDATED NumberCode",
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            updateDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                Id = originalDocument.ResourceReceiptEntities.First().Id,
                ResourceId = resource1.Id,
                MeasureUnitId = muPiece.Id,
                Quantity = 150
            });

            updateDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource3.Id,
                MeasureUnitId = muKg.Id,
                Quantity = 300
            });

            // Act
            await resourceReceiptRepository.UpdateResourceReceiptAsync(updateDocument);
        }
    }
}
