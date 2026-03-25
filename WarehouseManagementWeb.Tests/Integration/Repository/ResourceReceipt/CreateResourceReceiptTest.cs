using Microsoft.EntityFrameworkCore;
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

            // Act
            await resourceReceiptRepository.CreateResourceReceiptAsync(document);

            // Assert 
            var resourcesIds = new int[] { resource1.Id, resource2.Id };
            var measureUnitIds = new int[] { unitKg.Id, unitPiece.Id };

            var balances = await applicationDbContext.Balances.Where(b => resourcesIds.Contains(b.ResourceId) &&
                measureUnitIds.Contains(b.MeasureUnitId)).ToListAsync();

            Assert.Equal(2, balances.Count);

            var balance1 = balances.FirstOrDefault(b => b.ResourceId == resource1.Id &&
                b.MeasureUnitId == unitPiece.Id);
            var balance2 = balances.FirstOrDefault(b => b.ResourceId == resource2.Id &&
                b.MeasureUnitId == unitKg.Id);

            var documentQuantity1 = document.ResourceReceiptEntities
                .FirstOrDefault(d => d.ResourceId == resource2.Id && 
                d.MeasureUnitId == unitKg.Id)!.Quantity;

            var documentQuantity2 = document.ResourceReceiptEntities
                .FirstOrDefault(d => d.ResourceId == resource1.Id && 
                d.MeasureUnitId == unitPiece.Id)!.Quantity;

            Assert.Equal(120, balance1!.Quantity);
            Assert.Equal(850, balance2!.Quantity);
            Assert.Equal(documentQuantity1, balance2.Quantity);
            Assert.Equal(documentQuantity2, balance1.Quantity);
        }

        [Fact]
        public async Task CreateResourceReceiptAsyncResourcesNullTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            var document = new DocumentReceiptEntity
            {
                NumberCode = faker.Random.AlphaNumeric(5),
                Date = DateTime.UtcNow,
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>()
            };

            // Act & Assert.
            await resourceReceiptRepository.CreateResourceReceiptAsync(document);
        }
    }
}
