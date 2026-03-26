using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class GetResourceReceiptsTest : BaseIntegrationTest
    {
        public GetResourceReceiptsTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetResourceReceiptsAsyncTest()
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
                ClientId = client.Id,
                ResourceReceiptEntities = new List<ResourceReceiptEntity>
                {
                    new ResourceReceiptEntity
                    {
                        ResourceId = resource1.Id,
                        MeasureUnitId = unitPiece.Id,
                        Quantity = 100
                    }
                }
            };

            await resourceReceiptRepository.CreateResourceReceiptAsync(document);

            // Act
            var result = await resourceReceiptRepository.GetResourceReceiptsAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
