using Moq;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentReceipt
{
    public class GetResourceReceiptsTest : BaseDocumentReceiptTest
    {
        [Fact]
        public async Task GetResourceReceiptsAsyncTest()
        {
            // Arrange
            var output = new List<ResourceReceiptListOutput>
            {
               new ResourceReceiptListOutput
               {
                     DocumentReceiptId = 1,
                     DocumentReceiptNumberCode = "Doc",
                     DocumentReceiptDate = DateTime.UtcNow,
                     DocumentReceiptClientName = "Тестовый клиент",
                     Items = new List<ResourceReceiptItemOutput>
                     {
                         new ResourceReceiptItemOutput
                         {
                             ResourceReceiptId = 1,
                             ResourceId = 1,
                             ResourceTitle = "Кирпич",
                             ResourceQuantity = 100
                         },
                          new ResourceReceiptItemOutput
                         {
                             ResourceReceiptId = 2,
                             ResourceId = 2,
                             ResourceTitle = "Ресурс",
                             ResourceQuantity = 200
                         },
                     },
                },
                new ResourceReceiptListOutput
               {
                     DocumentReceiptId = 2,
                     DocumentReceiptNumberCode = "Doc2",
                     DocumentReceiptDate = DateTime.UtcNow,
                     DocumentReceiptClientName = "Тестовый клиент2",
                     Items = new List<ResourceReceiptItemOutput>
                     {
                         new ResourceReceiptItemOutput
                         {
                             ResourceReceiptId = 1,
                             ResourceId = 1,
                             ResourceTitle = "Кирпич",
                             ResourceQuantity = 100
                         },
                          new ResourceReceiptItemOutput
                         {
                             ResourceReceiptId = 2,
                             ResourceId = 2,
                             ResourceTitle = "Ресурс",
                             ResourceQuantity = 200
                         },
                     },
                }
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.GetResourceReceiptsAsync())
                .ReturnsAsync(output);

            // Act
            var result = await documentReceiptService.GetResourceReceiptsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, output);
            Assert.Equal(result.Count(), output.Count());

            mockDocumentReceiptRepository.Verify(repo => repo.GetResourceReceiptsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetResourceReceiptsAsyncNullTest()
        {
            // Arrange
            List<ResourceReceiptListOutput> output = null;

            mockDocumentReceiptRepository
                .Setup(repo => repo.GetResourceReceiptsAsync())
                .ReturnsAsync(output);

            // Act
            var result = await documentReceiptService.GetResourceReceiptsAsync();

            // Assert
            Assert.Null(result);

            mockDocumentReceiptRepository.Verify(repo => repo.GetResourceReceiptsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetResourceReceiptsAsyncThrowExceptionTest()
        {
            // Arrange
            var exception = new Exception("Ошибка");

            mockDocumentReceiptRepository
                        .Setup(repo => repo.GetResourceReceiptsAsync())
                        .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(documentReceiptService.GetResourceReceiptsAsync);

            mockDocumentReceiptRepository.Verify(repo => repo.GetResourceReceiptsAsync(), Times.Once);
        }
    }
}
