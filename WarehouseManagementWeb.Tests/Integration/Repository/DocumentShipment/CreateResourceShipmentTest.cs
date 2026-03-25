using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.DocumentShipment
{
    public class CreateResourceShipmentTest : BaseIntegrationTest
    {
        public CreateResourceShipmentTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CreateResourceShipmentAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            var resource1 = await SeedResourceAsync();
            var resource2 = await SeedResourceAsync();

            var unitKg = await SeedMeasureUnitAsync("кг");
            var unitPiece = await SeedMeasureUnitAsync("шт");

            await SeedBalancesAsync(resource1, unitPiece, 100);
            await SeedBalancesAsync(resource2, unitKg, 200);

            var document = new DocumentShipmentEntity
            {
                NumberCode = faker.Random.AlphaNumeric(5).ToUpper(),
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

            // Act
            await documentShipmentRepository.CreateResourceShipmentAsync(document);

            // Assert         
            var savedDocument = await applicationDbContext.DocumentShipments
                .Include(d => d.ResourceShipmentEntities)
                .FirstOrDefaultAsync(d => d.NumberCode == document.NumberCode);

            Assert.NotNull(savedDocument);
            Assert.Equal(2, savedDocument.ResourceShipmentEntities.Count);

            var updatedBalance1 = await applicationDbContext.Balances
                .FirstOrDefaultAsync(b => b.ResourceId == resource1.Id);

            var updatedBalance2 = await applicationDbContext.Balances
                .FirstOrDefaultAsync(b => b.ResourceId == resource2.Id);

            Assert.Equal(0, updatedBalance1!.Quantity);
            Assert.Equal(10, updatedBalance2!.Quantity);
        }
    }
}
