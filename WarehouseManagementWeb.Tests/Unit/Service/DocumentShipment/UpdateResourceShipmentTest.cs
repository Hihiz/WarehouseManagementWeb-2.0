using Moq;
using WarehouseManagementWeb.Application.Dto.Input.ResourceShipment;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Tests.Unit.Service.DocumentShipment
{
    public class UpdateResourceShipmentTest : BaseDocumentShipmentTest
    {
        [Fact]
        public async Task UpdateResourceShipmentAsyncTest()
        {
            // Arrange
            var input = new UpdateResourceShipmentInput
            {
                DocumentShipmentId = 1,
                DocumentShipmentNumberCode = "DOC1",
                DocumentShipmentDate = DateTime.UtcNow,
                DocumentShipmentClientId = 1,
                IsSetActiveStatus = false,
                ModifyResourceShipmentInputs = new List<ModifyResourceShipmentInput>
                {
                      new ModifyResourceShipmentInput()
                    {
                       ResourceShipmentId = 1,
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new ModifyResourceShipmentInput()
                    {
                       ResourceShipmentId = 2,
                       ResourceId = 2,
                       MeasureUnitId = 2,
                       ResourceQuantity = 200
                    },
                            new ModifyResourceShipmentInput()
                    {
                       ResourceId = 3,
                       MeasureUnitId = 2,
                       ResourceQuantity = 300
                    }
                }
            };

            mockDocumentShipmentRepository
                .Setup(repo => repo.CheckDocumentShipmentExistsByIdAndNumberCodeAsync(input.DocumentShipmentId,
                input.DocumentShipmentNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await documentShipmentService.UpdateResourceShipmentAsync(input);

            mockDocumentShipmentRepository.Verify(r => r.UpdateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateResourceShipmentAsyncDuplicateResourcesTest()
        {
            // Arrange
            var input = new UpdateResourceShipmentInput
            {
                DocumentShipmentId = 1,
                ModifyResourceShipmentInputs = new List<ModifyResourceShipmentInput>
                {
                      new ModifyResourceShipmentInput()
                    {
                       ResourceShipmentId = 1,
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    },
                         new ModifyResourceShipmentInput()
                    {
                       ResourceId = 1,
                       MeasureUnitId = 2,
                       ResourceQuantity = 100
                    }
                }
            };

            mockDocumentShipmentRepository
                .Setup(repo => repo.CheckDocumentShipmentExistsByIdAndNumberCodeAsync(input.DocumentShipmentId,
                    input.DocumentShipmentNumberCode))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentShipmentService.UpdateResourceShipmentAsync(input));

            mockDocumentShipmentRepository.Verify(r => r.UpdateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Never);
        }

        [Fact]
        public async Task UpdateResourceShipmentAsyncNullTest()
        {
            // Arrange
            UpdateResourceShipmentInput input = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentShipmentService.UpdateResourceShipmentAsync(input));

            mockDocumentShipmentRepository.Verify(r => r.UpdateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Never);
        }


        [Fact]
        public async Task UpdateResourceShipmentAsyncDuplicateNumberCodeTest()
        {
            // Arrange
            var input = new UpdateResourceShipmentInput
            {
                DocumentShipmentId = 1,
                DocumentShipmentNumberCode = "DOC1"
            };

            mockDocumentShipmentRepository
               .Setup(repo => repo.CheckDocumentShipmentExistsByIdAndNumberCodeAsync(input.DocumentShipmentId,
                   input.DocumentShipmentNumberCode))
               .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => documentShipmentService.UpdateResourceShipmentAsync(input));

            mockDocumentShipmentRepository.Verify(r => r.UpdateResourceShipmentAsync(
                It.IsAny<DocumentShipmentEntity>()), Times.Never);
        }
    }
}
