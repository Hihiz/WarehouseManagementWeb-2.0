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
            var client = new ClientEntity
            {
                Name = faker.Company.CompanyName(),
                Address = faker.Address.FullAddress()
            };
            await clientRepository.CreateClientAsync(client);

            var resource1 = new ResourceEntity { Title = faker.Commerce.ProductName() };
            var resource2 = new ResourceEntity { Title = faker.Commerce.ProductName() };
            await resourceRepository.CreateResourceAsync(resource1);
            await resourceRepository.CreateResourceAsync(resource2);

            var unitPiece = new MeasureUnitEntity { Title = "шт" };
            var unitKg = new MeasureUnitEntity { Title = "кг" };
            await measureUnitRepository.CreateMeasureUnitAsync(unitPiece);
            await measureUnitRepository.CreateMeasureUnitAsync(unitKg);

            var balance1 = new BalanceEntity
            {
                ResourceId = resource1.Id,
                MeasureUnitId = unitPiece.Id,
                Quantity = 100
            };
            await applicationDbContext.Balances.AddRangeAsync(balance1);
            await applicationDbContext.SaveChangesAsync();

            var document = new DocumentReceiptEntity
            {
                NumberCode = faker.Random.AlphaNumeric(5).ToUpper(),
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
            Assert.Equal(1, document.Id);
            Assert.Equal(200, balance1.Quantity);
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
