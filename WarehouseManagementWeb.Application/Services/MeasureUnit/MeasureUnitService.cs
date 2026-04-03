using Microsoft.Extensions.Logging;
using WarehouseManagementWeb.Application.Dto.Input.MeasureUnit;
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
        public async Task<MeasureUnitListByStatusOutput> GetMeasureUnitsAsync()
        {
            try
            {
                IEnumerable<MeasureUnitOutput> measureUnits = await _measureUnitRepository.GetMeasureUnitsAsync();

                List<MeasureUnitOutput> activeMeasureUnits = new List<MeasureUnitOutput>(measureUnits.Count(
                    r => r.MeasureUnitStatusEnum == DirectoryStatusEnum.Active));

                List<MeasureUnitOutput> archivedMeasureUnits = new List<MeasureUnitOutput>(measureUnits.Count(
                    r => r.MeasureUnitStatusEnum == DirectoryStatusEnum.Archived));

                foreach (var measureUnit in measureUnits)
                {
                    switch (measureUnit.MeasureUnitStatusEnum)
                    {
                        case DirectoryStatusEnum.Active:
                            activeMeasureUnits.Add(measureUnit);
                            break;

                        case DirectoryStatusEnum.Archived:
                            archivedMeasureUnits.Add(measureUnit);
                            break;
                    }
                }

                MeasureUnitListByStatusOutput result = new MeasureUnitListByStatusOutput
                {
                    ActiveMeasureUnits = activeMeasureUnits.OrderByDescending(r => r.Id).ToList(),
                    ArchivedMeasureUnits = archivedMeasureUnits.OrderByDescending(r => r.Id).ToList()
                };

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MeasureUnitOutput>> GetActiveMeasureUnitsAsync(int[]? measureUnitId)
        {
            try
            {
                IEnumerable<MeasureUnitOutput> result = await _measureUnitRepository.GetActiveMeasureUnitsAsync(
                    measureUnitId);

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<MeasureUnitOutput?> GetMeasureUnitByIdAsync(int measureUnitId)
        {
            try
            {
                if (measureUnitId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id единицы измерения. " +
                                                        $"MeasureUnitId: {measureUnitId}.");
                }

                MeasureUnitOutput? result = await _measureUnitRepository.GetMeasureUnitByIdAsync(measureUnitId);

                if (result is null)
                {
                    return new MeasureUnitOutput();
                }

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task CreateMeasureUnitAsync(CreateMeasureUnitInput createMeasureUnitInput)
        {
            try
            {
                if (createMeasureUnitInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные единицы измерения.");
                }

                bool isMeasureUnitTitleExist = await _measureUnitRepository.CheckMeasureUnitExistsByTitleAsync(
                    createMeasureUnitInput.Title!);

                if (isMeasureUnitTitleExist)
                {
                    throw new InvalidOperationException("Единица измерения с наименованием: " +
                                                        $"'{createMeasureUnitInput.Title}' уже существует в системе.");
                }

                MeasureUnitEntity entity = new MeasureUnitEntity
                {
                    Title = createMeasureUnitInput.Title!
                };

                await _measureUnitRepository.CreateMeasureUnitAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateMeasureUnitAsync(UpdateMeasureUnitInput updateMeasureUnitInput)
        {
            try
            {
                if (updateMeasureUnitInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные единицы измерения.");
                }

                bool isMeasureUnitTitleExist = await _measureUnitRepository.CheckMeasureUnitExistsByIdAndTitleAsync(
                  updateMeasureUnitInput.Id, updateMeasureUnitInput.Title!);

                if (isMeasureUnitTitleExist)
                {
                    throw new InvalidOperationException("Единица измерения с наименованием: " +
                                                        $"'{updateMeasureUnitInput.Title}' уже существует в системе.");
                }

                MeasureUnitEntity entity = new MeasureUnitEntity
                {
                    Id = updateMeasureUnitInput.Id,
                    Title = updateMeasureUnitInput.Title!
                };

                await _measureUnitRepository.UpdateMeasureUnitAsync(entity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
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

        /// <inheritdoc />
        public async Task RemoveMeasureUnitAsync(int measureUnitId)
        {
            try
            {
                if (measureUnitId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id единицы измерения. " +
                                                        $"MeasureUnitId: {measureUnitId}.");
                }

                await _measureUnitRepository.RemoveMeasureUnitAsync(measureUnitId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}
