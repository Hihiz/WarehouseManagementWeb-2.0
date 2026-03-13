using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Repositories.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Services.MeasureUnit;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

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


        /// <inheritdoc />
        public async Task ChangeStatusMeasureUnitAsync(int measureUnitId, DirectoryStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task CreateMeasureUnitAsync(MeasureUnitEntity measureUnitEntity)
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

        public Task<MeasureUnitListByStatusOutput> GetMeasureUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveMeasureUnitAsync(int measureUnitId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMeasureUnitAsync(MeasureUnitEntity measureUnitEntity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
