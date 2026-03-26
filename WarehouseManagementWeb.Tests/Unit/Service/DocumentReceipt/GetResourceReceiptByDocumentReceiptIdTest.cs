using Moq;
using WarehouseManagementWeb.Application.Dto.Output.ResourceReceipt;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentReceipt
{
    public class GetResourceReceiptByDocumentReceiptIdTest : BaseDocumentReceiptTest
    {
        [Fact]
        public async Task GetResourceReceiptByDocumentReceiptIdAsyncTest()
        {
            // Arrange
            var output = new ResourceReceiptListOutput
            {
                DocumentReceiptId = 1,
                DocumentReceiptNumberCode = "Doc",
                DocumentReceiptDate = DateTime.UtcNow,
                DocumentReceiptClientName = "Тестовый клиент",
                Items = new List<ResourceReceiptItemOutput>
                {
                    new ResourceReceiptItemOutput
                    {
                        ResourceId = 1,
                        ResourceTitle = "Кирпич",
                        ResourceQuantity = 100
                    }
                }
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.GetResourceReceiptByDocumentReceiptIdAsync(output.DocumentReceiptId))
                .ReturnsAsync(output);

            // Act
            var result = await documentReceiptService.GetResourceReceiptByDocumentReceiptIdAsync(
                output.DocumentReceiptId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.DocumentReceiptId);
            Assert.Single(result.Items);

            mockDocumentReceiptRepository.Verify(repo => repo.GetResourceReceiptByDocumentReceiptIdAsync(
                output.DocumentReceiptId), Times.Once);
        }

        [Fact]
        public async Task GetResourceReceiptByDocumentReceiptIdAsyncThrowExceptionTest()
        {
            // Arrange
            var id = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
               () => documentReceiptService.GetResourceReceiptByDocumentReceiptIdAsync(id));

            mockDocumentReceiptRepository.Verify(repo => repo.GetResourceReceiptByDocumentReceiptIdAsync(id),
                Times.Never);
        }

        [Fact]
        public async Task GetResourceReceiptByDocumentReceiptIdAsyncNullTest()
        {
            // Arrange
            var id = 1;
            ResourceReceiptListOutput output = null;
            
            mockDocumentReceiptRepository
                .Setup(repo => repo.GetResourceReceiptByDocumentReceiptIdAsync(1))
                .ReturnsAsync(output);

            // Act & Assert
            var result = await documentReceiptService.GetResourceReceiptByDocumentReceiptIdAsync(id);

            mockDocumentReceiptRepository.Verify(repo => repo.GetResourceReceiptByDocumentReceiptIdAsync(id),
                Times.Once);
        }
    }
}
