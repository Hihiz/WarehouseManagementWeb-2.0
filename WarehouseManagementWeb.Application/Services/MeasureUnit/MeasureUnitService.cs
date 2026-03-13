using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.MeasureUnit;
using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Repositories.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Services.MeasureUnit;

namespace WarehouseManagementWeb.Application.Services.MeasureUnit
{
    /// <summary>
    /// Класс реализует методы сервиса единиц измерений.
    /// </summary>
    public class MeasureUnitService : IMeasureUnitService
    {
        private readonly IMeasureUnitRepository _measureUnitRepository;
        private readonly ILogger<MeasureUnitService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="measureUnitRepository">Репозиторий единиц измерений.</param>
        /// <param name="logger">Логгер.</param>
        public MeasureUnitService(IMeasureUnitRepository measureUnitRepository,
            ILogger<MeasureUnitService> logger)
        {
            _measureUnitRepository = measureUnitRepository;
            _logger = logger;
        }

        #region Публичные методы.

        public Task<MeasureUnitListByStatusOutput> GetMeasureUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeasureUnitOutput>> GetActiveMeasureUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MeasureUnitOutput?> GetMeasureUnitByIdAsync(int measureUnitId)
        {
            throw new NotImplementedException();
        }

        public Task CreateMeasureUnitAsync(CreateMeasureUnitInput createMeasureUnitInput)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMeasureUnitAsync(UpdateMeasureUnitInput updateMeasureUnitInput)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task ChangeStatusMeasureUnitAsync(ChangeStatusMeasureUnitInput changeStatusMeasureUnitInput)
        {
            try
            {
                if (changeStatusMeasureUnitInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные ресурса.");
                }

                await _measureUnitRepository.ChangeStatusMeasureUnitAsync(changeStatusMeasureUnitInput.MeasureUnitId,
                    changeStatusMeasureUnitInput.MeasureUnitStatusEnum);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        public Task RemoveMeasureUnitAsync(int measureUnitId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
