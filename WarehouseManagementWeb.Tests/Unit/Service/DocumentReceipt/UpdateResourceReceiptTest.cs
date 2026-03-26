using Moq;
using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentReceipt
{
    public class UpdateResourceReceiptTest : BaseDocumentReceiptTest
    {
        [Fact]
        public async Task UpdateResourceReceiptTestAsync()
        {
            // Arrange
            var input = new UpdateResourceReceiptInput
            {
                DocumentReceiptId = 1,
                DocumentReceiptNumberCode = "DOC1",
                Date = DateTime.UtcNow,
                DocumentReceiptClientId = 1,
                ModifyResourceReceiptInputs = new List<ModifyResourceReceiptInput>
                {
                      new ModifyResourceReceiptInput()
                    {
                       ResourceReceiptId = 1,
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new ModifyResourceReceiptInput()
                    {
                       ResourceReceiptId = 2,
                       ResourceId = 2,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    },
                            new ModifyResourceReceiptInput()
                    {
                       ResourceId = 3,
                       MeasureUnitId = 2,
                       ResourceQuantity = 300
                    }
                }
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.CheckDocumentReceiptExistsByIdAndNumberCodeAsync(input.DocumentReceiptId,
                    input.DocumentReceiptNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await documentReceiptService.UpdateResourceReceiptAsync(input);

            mockDocumentReceiptRepository.Verify(r => r.UpdateResourceReceiptAsync(
                It.IsAny<DocumentReceiptEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateResourceReceiptDuplicateResourcesTest()
        {
            // Arrange
            var input = new UpdateResourceReceiptInput
            {
                DocumentReceiptNumberCode = "DOC1",
                ModifyResourceReceiptInputs = new List<ModifyResourceReceiptInput>
                {
                    new ModifyResourceReceiptInput()
                    {
                       ResourceReceiptId = 1,
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                    new ModifyResourceReceiptInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    }
                }
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.CheckDocumentReceiptExistsByIdAndNumberCodeAsync(input.DocumentReceiptId,
                    input.DocumentReceiptNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
               () => documentReceiptService.UpdateResourceReceiptAsync(input));

            mockDocumentReceiptRepository.Verify(r => r.UpdateResourceReceiptAsync(
                It.IsAny<DocumentReceiptEntity>()), Times.Never);
        }

        [Fact]
        public async Task UpdateResourceReceiptAsyncNullTest()
        {
            // Arrange
            UpdateResourceReceiptInput input = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
               () => documentReceiptService.UpdateResourceReceiptAsync(input));

            mockDocumentReceiptRepository.Verify(r => r.UpdateResourceReceiptAsync(
                 It.IsAny<DocumentReceiptEntity>()), Times.Never);
        }

        [Fact]
        public async Task UpdateResourceReceiptAsyncDuplicateNumberCodeTest()
        {
            // Arrange
            var input = new UpdateResourceReceiptInput
            {
                DocumentReceiptId = 1,
                DocumentReceiptNumberCode = "DOC1"
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.CheckDocumentReceiptExistsByIdAndNumberCodeAsync(input.DocumentReceiptId,
                    input.DocumentReceiptNumberCode))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentReceiptService.UpdateResourceReceiptAsync(input));

            mockDocumentReceiptRepository.Verify(r => r.UpdateResourceReceiptAsync(
                It.IsAny<DocumentReceiptEntity>()), Times.Never);
        }
    }
}
