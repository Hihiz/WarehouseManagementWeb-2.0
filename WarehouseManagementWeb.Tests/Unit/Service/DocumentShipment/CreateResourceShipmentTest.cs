using Moq;
using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentShipment
{
    public class CreateResourceShipmentTest : BaseDocumentShipmentTest
    {
        [Fact]
        public async Task CreateResourceShipmentAsyncTest()
        {
            // Arrange
            var input = new CreateResourceShipmentInput
            {
                DocumentShipmentNumberCode = "DOC1",
                DocumentShipmentDate = DateTime.UtcNow,
                DocumentShipmentClientId = 1,
                IsSetActiveStatus = false,
                IncludeResourceShipmentInputs = new List<IncludeResourceShipmentInput>
                {
                      new IncludeResourceShipmentInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new IncludeResourceShipmentInput()
                    {
                       ResourceId = 2,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    },
                            new IncludeResourceShipmentInput()
                    {
                       ResourceId = 3,
                       MeasureUnitId = 2,
                       ResourceQuantity = 300
                    }
                }
            };

            mockDocumentShipmentRepository
                .Setup(repo => repo.CheckDocumentShipmentExistsByNumberCodeAsync(input.DocumentShipmentNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await documentShipmentService.CreateResourceShipmentAsync(input);

            mockDocumentShipmentRepository.Verify(r => r.CreateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateResourceShipmentAsyncDuplicateResourcesTest()
        {
            // Arrange
            var input = new CreateResourceShipmentInput
            {
                IncludeResourceShipmentInputs = new List<IncludeResourceShipmentInput>
                {
                      new IncludeResourceShipmentInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new IncludeResourceShipmentInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    }
                }
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentShipmentService.CreateResourceShipmentAsync(input));

            mockDocumentShipmentRepository.Verify(r => r.CreateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Never);
        }

        [Fact]
        public async Task CreateResourceShipmentAsyncNullTest()
        {
            // Arrange
            CreateResourceShipmentInput input = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentShipmentService.CreateResourceShipmentAsync(input));

            mockDocumentShipmentRepository.Verify(r => r.CreateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Never);
        }


        [Fact]
        public async Task CreateResourceShipmentAsyncDuplicateNumberCodeTest()
        {
            // Arrange
            var input = new CreateResourceShipmentInput
            {
                DocumentShipmentNumberCode = "DOC1"
            };

            mockDocumentShipmentRepository
                .Setup(repo => repo.CheckDocumentShipmentExistsByNumberCodeAsync(input.DocumentShipmentNumberCode))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentShipmentService.CreateResourceShipmentAsync(input));

            mockDocumentShipmentRepository.Verify(r => r.CreateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Never);
        }
    }
}
