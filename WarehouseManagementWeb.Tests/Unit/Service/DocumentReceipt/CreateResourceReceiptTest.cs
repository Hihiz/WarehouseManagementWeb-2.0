using Moq;
using WarehouseManagementWeb.Application.Dto.Input.ResourceReceipt;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentReceipt
{
    public class CreateResourceReceiptTest : BaseDocumentReceiptTest
    {
        [Fact]
        public async Task CreateResourceReceiptTestAsync()
        {
            // Arrange
            var input = new CreateResourceReceiptInput
            {
                DocumentReceiptNumberCode = "DOC1",
                Date = DateTime.UtcNow,
                ClientId = 1,
                IncludeResourceReceiptInputs = new List<IncludeResourceReceiptInput>
                {
                      new IncludeResourceReceiptInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new IncludeResourceReceiptInput()
                    {
                       ResourceId = 2,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    },
                            new IncludeResourceReceiptInput()
                    {
                       ResourceId = 3,
                       MeasureUnitId = 2,
                       ResourceQuantity = 300
                    }
                }
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.CheckDocumentReceiptExistsByNumberCodeAsync(input.DocumentReceiptNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await documentReceiptService.CreateResourceReceiptAsync(input);

            mockDocumentReceiptRepository.Verify(r => r.CreateResourceReceiptAsync(
                It.IsAny<DocumentReceiptEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateResourceReceiptAsyncDuplicateResourcesTest()
        {
            // Arrange
            var input = new CreateResourceReceiptInput
            {
                DocumentReceiptNumberCode = "DOC1",

                IncludeResourceReceiptInputs = new List<IncludeResourceReceiptInput>
                {
                      new IncludeResourceReceiptInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new IncludeResourceReceiptInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    }
                }
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.CheckDocumentReceiptExistsByNumberCodeAsync(input.DocumentReceiptNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentReceiptService.CreateResourceReceiptAsync(input));

            mockDocumentReceiptRepository.Verify(r => r.CreateResourceReceiptAsync(
                It.IsAny<DocumentReceiptEntity>()), Times.Never);
        }

        [Fact]
        public async Task CreateResourceReceiptAsyncNullTest()
        {
            // Arrange
            CreateResourceReceiptInput input = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
               () => documentReceiptService.CreateResourceReceiptAsync(input));

            mockDocumentReceiptRepository.Verify(r => r.CreateResourceReceiptAsync(
                 It.IsAny<DocumentReceiptEntity>()), Times.Never);
        }


        [Fact]
        public async Task CreateResourceReceiptAsyncDuplicateNumberCodeTest()
        {
            // Arrange
            var input = new CreateResourceReceiptInput
            {
                DocumentReceiptNumberCode = "DOC1"
            };

            mockDocumentReceiptRepository
                .Setup(repo => repo.CheckDocumentReceiptExistsByNumberCodeAsync(input.DocumentReceiptNumberCode))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentReceiptService.CreateResourceReceiptAsync(input));

            mockDocumentReceiptRepository.Verify(r => r.CreateResourceReceiptAsync(
                It.IsAny<DocumentReceiptEntity>()), Times.Never);
        }
    }
}
