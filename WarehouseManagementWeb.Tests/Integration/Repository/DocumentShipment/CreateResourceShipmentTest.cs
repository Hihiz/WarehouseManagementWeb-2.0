using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.DocumentShipment
{
    public class CreateResourceShipmentTest : BaseIntegrationTest
    {
        [Fact]
        public async void CreateResourceShipmentAsyncTest()
        {
            // Arrange
            var client = new ClientEntity
            {
                Name = "Клиент для отгрузки " + Guid.NewGuid().ToString(),
                Address = "Тестовый адрес" + Guid.NewGuid().ToString()
            };
            await clientRepository.CreateClientAsync(client);

            var resource1 = new ResourceEntity { Title = "Кирпич М100 " + Guid.NewGuid().ToString() };
            var resource2 = new ResourceEntity { Title = "Цемент М500 " + Guid.NewGuid().ToString() };
            await resourceRepository.CreateResourceAsync(resource1);
            await resourceRepository.CreateResourceAsync(resource2);

            var unitPiece = new MeasureUnitEntity { Title = "шт" + Guid.NewGuid().ToString() };
            var unitKg = new MeasureUnitEntity { Title = "кг" + Guid.NewGuid().ToString() };
            await measureUnitRepository.CreateMeasureUnitAsync(unitPiece);
            await measureUnitRepository.CreateMeasureUnitAsync(unitKg);

            var balance1 = new BalanceEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 100
            };
            var balance2 = new BalanceEntity
            {
                ResourceId = resource2.Id,
                MeasureUnitId = unitKg.Id,
                Quantity = 200
            };
            await applicationDbContext.Balances.AddRangeAsync(balance1, balance2);
            await applicationDbContext.SaveChangesAsync();

            var document = new DocumentShipmentEntity
            {
                NumberCode = "test Number Code" + Guid.NewGuid().ToString(),
                ClientId = client.Id,
                ResourceShipmentEntities = new List<ResourceShipmentEntity>
                {
                   new ResourceShipmentEntity
                        {
                            ResourceId = resource1.Id,
                            MeasureUnitId = unitPiece.Id,
                            Quantity = 100
                        },
                        new ResourceShipmentEntity
                        {
                            ResourceId = resource2.Id,
                            MeasureUnitId = unitKg.Id,
                            Quantity = 190
                        }
                }
            };

            // Act & Assert
            await documentShipmentRepository.CreateResourceShipmentAsync(document);
        }
    }
}
