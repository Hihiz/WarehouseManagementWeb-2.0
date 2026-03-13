using WarehouseManagementWeb.Application.Dto.Input.MeasureUnit;
using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;

namespace WarehouseManagementWeb.Application.Interfaces.Services.MeasureUnit
{
    /// <summary>
    /// Интерфейс сервиса единиц измерений.
    /// </summary>
    public interface IMeasureUnitService 
    {
        /// <summary>
        /// Метод получает список единиц измерений.
        /// </summary>
        /// <returns>Список единиц измерений.</returns>
        Task<MeasureUnitListByStatusOutput> GetMeasureUnitsAsync();

        /// <summary>
        /// Метод получает список активных единиц измерения.
        /// </summary>
        /// <returns>Список активных единиц измерения.</returns>
        Task<IEnumerable<MeasureUnitOutput>> GetActiveMeasureUnitsAsync();

        /// <summary>
        /// Метод получает единицу измерения по Id.
        /// </summary>
        /// <param name="measureUnitId">Id единицы измерения.</param>
        /// <returns>Данные единицы измерения.</returns>
        Task<MeasureUnitOutput?> GetMeasureUnitByIdAsync(int measureUnitId);

        /// <summary>
        /// Метод добавляет единицу измерения.
        /// </summary>
        /// <param name="createMeasureUnitInput">Входная модель.</param>
        Task CreateMeasureUnitAsync(CreateMeasureUnitInput createMeasureUnitInput);

        /// <summary>
        /// Метод редактирует единицу измерения.
        /// </summary>
        /// <param name="updateMeasureUnitInput">Входная модель.</param>
        Task UpdateMeasureUnitAsync(UpdateMeasureUnitInput updateMeasureUnitInput);

        /// <summary>
        /// Метод обновляет статус единице измерения.
        /// </summary>
        /// <param name="changeStatusMeasureUnitInput">Входная модель.</param>
        Task ChangeStatusMeasureUnitAsync(ChangeStatusMeasureUnitInput changeStatusMeasureUnitInput);

        /// <summary>
        /// Метод удаляет единицу измерения.
        /// </summary>
        /// <param name="measureUnitId">Id единицы измерения.</param>
        Task RemoveMeasureUnitAsync(int measureUnitId);
    }
}
