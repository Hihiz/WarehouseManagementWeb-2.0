using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class UpdateResourceReceiptTest : BaseIntegrationTest
    {
        public UpdateResourceReceiptTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task UpdateResourceReceiptAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            var resource1 = await SeedResourceAsync();
            var resource2 = await SeedResourceAsync();
            var resource3 = await SeedResourceAsync();

            var unitKg = await SeedMeasureUnitAsync("кг");
            var unitPiece = await SeedMeasureUnitAsync("шт");

            var originalDocument = new DocumentReceiptEntity
            {
                NumberCode = faker.Random.AlphaNumeric(5),
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            originalDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = unitKg.Id,
                Quantity = 100
            });

            originalDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource2.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 500
            });

            await resourceReceiptRepository.CreateResourceReceiptAsync(originalDocument);

            var updateDocument = new DocumentReceiptEntity
            {
                Id = originalDocument.Id,
                NumberCode = faker.Random.AlphaNumeric(5),
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            updateDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                Id = 1,
                ResourceId = resource1.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 150
            });

            updateDocument.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource3.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 300
            });

            // Act
            await resourceReceiptRepository.UpdateResourceReceiptAsync(updateDocument);

            // Assert 
            var resourcesIds = new int[] { resource1.Id, resource2.Id, resource3.Id };
            var measureUnitIds = new int[] { unitKg.Id, unitPiece.Id };

            var balances = await applicationDbContext.Balances.Where(b => resourcesIds.Contains(b.ResourceId) &&
                measureUnitIds.Contains(b.MeasureUnitId)).ToListAsync();

            Assert.Equal(4, balances.Count);

            var balance1 = GetBalance(balances, resource1.Id, unitPiece.Id);
            var balance2 = GetBalance(balances, resource2.Id, unitKg.Id);
            var balance3 = GetBalance(balances, resource3.Id, unitPiece.Id);
            var balance4 = GetBalance(balances, resource2.Id, unitPiece.Id);

            Assert.Equal(150, balance1.Quantity);
            Assert.Null(balance2);
            Assert.Equal(300, balance3.Quantity);
            Assert.Equal(0, balance4.Quantity);

            Assert.Equal(originalDocument.ResourceReceiptEntities.First().Quantity, balance1.Quantity);

            var documentQuantity1 = GetResourceReceipt(updateDocument.ResourceReceiptEntities.ToList(), resource1.Id,
                unitPiece.Id);
            var documentQuantity2 = GetResourceReceipt(updateDocument.ResourceReceiptEntities.ToList(), resource3.Id,
                unitPiece.Id);
            var documentQuantity3 = GetResourceReceipt(updateDocument.ResourceReceiptEntities.ToList(), resource2.Id,
                unitPiece.Id);

            Assert.Equal(documentQuantity1.Quantity, balance1.Quantity);
            Assert.Equal(documentQuantity2.Quantity, balance3.Quantity);

            Assert.Null(documentQuantity3);
        }
    }
}
