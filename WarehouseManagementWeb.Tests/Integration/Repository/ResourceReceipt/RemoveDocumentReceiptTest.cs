using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class RemoveDocumentReceiptTest : BaseIntegrationTest
    {
        public RemoveDocumentReceiptTest(DatabaseFixture fixture) : base(fixture) { }


        [Fact]
        public async Task RemoveDocumentReceiptAsyncTest()
        {
            // Arrange
            // 1. Клиент
            var client = new ClientEntity
            {
                Name = "Клиент для теста удаления " + Guid.NewGuid().ToString()[..8],
                Address = "Тестовый адрес"
            };
            await clientRepository.CreateClientAsync(client);

            // 2. Ресурсы
            var resource1 = new ResourceEntity { Title = "Кирпич М100 " + Guid.NewGuid().ToString()[..4] };
            var resource2 = new ResourceEntity { Title = "Цемент М500 " + Guid.NewGuid().ToString()[..4] };
            await resourceRepository.CreateResourceAsync(resource1);
            await resourceRepository.CreateResourceAsync(resource2);

            // 3. Единицы измерения
            var unitPiece = new MeasureUnitEntity { Title = "ште" };
            var unitKg = new MeasureUnitEntity { Title = "кге" };
            await measureUnitRepository.CreateMeasureUnitAsync(unitPiece);
            await measureUnitRepository.CreateMeasureUnitAsync(unitKg);

            // 4. Начальный баланс на складе (до поступления)
            var initialBalance1 = new BalanceEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 200  // было 200 шт
            };
            var initialBalance2 = new BalanceEntity
            {
                ResourceId = resource2.Id,
                MeasureUnitId = unitKg.Id,
                Quantity = 800  // было 800 кг
            };
            await applicationDbContext.Balances.AddRangeAsync(initialBalance1, initialBalance2);

            // 5. Создаём документ поступления (чтобы потом его удалить)
            var document = new DocumentReceiptEntity
            {
                NumberCode = "POSTUP-" + Guid.NewGuid().ToString()[..8],
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>
                {
                    new ResourceReceiptEntity
                    {
                        ResourceId = resource1.Id,
                        MeasureUnitId = unitPiece.Id,
                        Quantity = 120
                    },
                    new ResourceReceiptEntity
                    {
                        ResourceId = resource2.Id,
                        MeasureUnitId = unitKg.Id,
                        Quantity = 300
                    }
                }
            };

            await resourceReceiptRepository.CreateResourceReceiptAsync(document);

            
            // Act — удаляем документ поступления
            await resourceReceiptRepository.RemoveDocumentReceiptAsync(document.Id);

            // Assert — проверяем результат после удаления

            // 1. Документ удалён
            var deletedDocument = await applicationDbContext.DocumentReceipts
                .FirstOrDefaultAsync(d => d.Id == document.Id);
            Assert.Null(deletedDocument);

            // 2. Все строки ресурсов удалены
            var remainingResources = await applicationDbContext.ResourceReceipts
                .CountAsync(rr => rr.DocumentReceiptId == document.Id);
            Assert.Equal(0, remainingResources);

            // 3. Баланс вернулся к исходному состоянию (ресурсы "вернулись" на склад)
            var balanceAfterRemove1 = await applicationDbContext.Balances
                .FirstAsync(b => b.ResourceId == resource1.Id && b.MeasureUnitId == unitPiece.Id);
            var balanceAfterRemove2 = await applicationDbContext.Balances
                .FirstAsync(b => b.ResourceId == resource2.Id && b.MeasureUnitId == unitKg.Id);

            Assert.Equal(200, balanceAfterRemove1.Quantity); // 320 - 120 = 200
            Assert.Equal(800, balanceAfterRemove2.Quantity); // 1100 - 300 = 800
        }
    }
}
