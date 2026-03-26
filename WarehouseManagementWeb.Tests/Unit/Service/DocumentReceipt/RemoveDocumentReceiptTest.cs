using Moq;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentReceipt
{
    public class RemoveDocumentReceiptTest : BaseDocumentReceiptTest
    {
        [Fact]
        public async Task RemoveDocumentReceiptAsyncTest()
        {
            // Arrange
            var id = 1;

            // Act
            await documentReceiptService.RemoveDocumentReceiptAsync(id);

            // Assert
            mockDocumentReceiptRepository.Verify(repo => repo.RemoveDocumentReceiptAsync(id), Times.Once);
        }

        [Fact]
        public async Task RemoveDocumentReceiptAsyncThrowExceptionTest()
        {
            // Arrange
            var id = 0;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                 () => documentReceiptService.RemoveDocumentReceiptAsync(id));

            mockDocumentReceiptRepository.Verify(repo => repo.RemoveDocumentReceiptAsync(It.IsAny<int>()),
                Times.Never);
        }
    }
}
