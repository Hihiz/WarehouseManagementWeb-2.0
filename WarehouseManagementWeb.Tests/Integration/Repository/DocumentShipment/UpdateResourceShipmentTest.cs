using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.DocumentShipment
{
    public class UpdateResourceShipmentTest : BaseIntegrationTest
    {
        public UpdateResourceShipmentTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task UpdateResourceShipmentAsyncTest()
        {
            // Arrange
            var client = await SeedClientAsync();

            var resource1 = await SeedResourceAsync();
            var resource2 = await SeedResourceAsync();
            var resource3 = await SeedResourceAsync();

            var unitKg = await SeedMeasureUnitAsync("кг");
            var unitPiece = await SeedMeasureUnitAsync("шт");

            await SeedBalancesAsync(resource1, unitPiece, 100);
            await SeedBalancesAsync(resource2, unitKg, 200);
            await SeedBalancesAsync(resource2, unitPiece, 200);
            await SeedBalancesAsync(resource3, unitKg, 200);

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
                    Quantity = 80
                },
                new ResourceShipmentEntity
                {
                    ResourceId = resource2.Id,
                    MeasureUnitId = unitKg.Id,
                    Quantity = 190
                }
            }
            };

            await documentShipmentRepository.CreateResourceShipmentAsync(document);

            // Act
            var updateDocument = new DocumentShipmentEntity
            {
                Id = document.Id,
                NumberCode = document.NumberCode,
                ClientId = document.ClientId,
                ResourceShipmentEntities = new List<ResourceShipmentEntity>
                {
                    new ResourceShipmentEntity
                    {
                        Id = 1,
                        ResourceId = resource1.Id,
                        MeasureUnitId = unitPiece.Id,
                        Quantity = 90
                    },
                    new ResourceShipmentEntity
                    {
                         Id = 2,
                        ResourceId = resource2.Id,
                        MeasureUnitId = unitPiece.Id,
                        Quantity = 190
                    },
                    new ResourceShipmentEntity
                    {
                        ResourceId = resource3.Id,
                        MeasureUnitId = unitKg.Id,
                        Quantity = 100
                    }
                }
            };

            await documentShipmentRepository.UpdateResourceShipmentAsync(updateDocument);

            // Assert         
            var savedDocument = await applicationDbContext.DocumentShipments
                .Include(d => d.ResourceShipmentEntities)
                .FirstOrDefaultAsync(d => d.NumberCode == document.NumberCode);

            Assert.NotNull(savedDocument);
            Assert.Equal(3, savedDocument.ResourceShipmentEntities.Count);

            var resourceIds = new[] { resource1.Id, resource2.Id, resource3.Id, };

            var updatedBalances = await applicationDbContext.Balances
                .Where(b => resourceIds.Contains(b.ResourceId)).ToListAsync();

            var updatedBalance1 = updatedBalances.FirstOrDefault(b => b.ResourceId == resource1.Id);
            var updatedBalance2 = updatedBalances.FirstOrDefault(b => b.ResourceId == resource2.Id);
            var updatedBalance3 = updatedBalances.FirstOrDefault(b => b.ResourceId == resource3.Id);

            Assert.Equal(10, updatedBalance1!.Quantity);
            Assert.Equal(200, updatedBalance2!.Quantity);
            Assert.Equal(100, updatedBalance3!.Quantity);
        }

        [Fact]
        public async Task UpdateResourceShipmentAsyncNullTest()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                   async () => await resourceReceiptRepository.UpdateResourceReceiptAsync(null));
        }
    }
}
