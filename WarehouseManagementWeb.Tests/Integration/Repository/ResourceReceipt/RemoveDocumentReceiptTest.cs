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
            var client = await SeedClientAsync();

            var resource1 = await SeedResourceAsync();
            var resource2 = await SeedResourceAsync();

            var unitKg = await SeedMeasureUnitAsync("кг");
            var unitPiece = await SeedMeasureUnitAsync("шт");

            var document = new DocumentReceiptEntity
            {
                NumberCode = faker.Random.AlphaNumeric(5),
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            document.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource2.Id,
                MeasureUnitId = unitKg.Id,
                Quantity = 850
            });
            document.ResourceReceiptEntities.Add(new ResourceReceiptEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 120
            });

           
            await resourceReceiptRepository.CreateResourceReceiptAsync(document);

            // Act
            await resourceReceiptRepository.RemoveDocumentReceiptAsync(document.Id);

            // Assert
            var resourcesIds = new int[] { resource1.Id, resource2.Id };
            var measureUnitIds = new int[] { unitKg.Id, unitPiece.Id };

            var balances = await applicationDbContext.Balances.Where(b => resourcesIds.Contains(b.ResourceId) &&
                measureUnitIds.Contains(b.MeasureUnitId)).ToListAsync();

            Assert.Equal(2, balances.Count);

            var balance1 = GetBalance(balances, resource1.Id, unitPiece.Id);
            var balance2 = GetBalance(balances, resource2.Id, unitKg.Id);

            Assert.Equal(0, balance1.Quantity);
            Assert.Equal(0, balance2.Quantity);

            var removedDocument = await applicationDbContext.DocumentReceipts.FirstOrDefaultAsync(d => d.Id == document.Id);
            Assert.Null(removedDocument);

        }
    }
}
