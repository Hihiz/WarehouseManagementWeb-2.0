using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Integration.Repository.ResourceReceipt
{
    public class GetResourceReceiptByDocumentReceiptIdTest : BaseIntegrationTest
    {
        public GetResourceReceiptByDocumentReceiptIdTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetResourceReceiptByDocumentReceiptIdAsyncTest()
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
            var result = await resourceReceiptRepository.GetResourceReceiptByDocumentReceiptIdAsync(document.Id);

            // Assert
            Assert.NotNull(result);
            Assert.True(document.Id > 0);
            Assert.Equal(100, document.ResourceReceiptEntities.First().Quantity);
        }

        [Fact]
        public async Task GetResourceReceiptByDocumentReceiptIdAsyncNotFoundTest()
        {
            // Act
            var documentId = 0;
                
            // Arrange
            var result = await resourceReceiptRepository.GetResourceReceiptByDocumentReceiptIdAsync(documentId);

            // Assert
            Assert.Null(result);
        }
    }
}
