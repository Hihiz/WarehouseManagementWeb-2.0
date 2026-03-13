using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

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
        /// <param name="measureUnitEntity">Модель единицы измерения.</param>
        Task CreateMeasureUnitAsync(MeasureUnitEntity measureUnitEntity);

        /// <summary>
        /// Метод редактирует единицу измерения.
        /// </summary>
        /// <param name="measureUnitEntity">Модель единицы измерения.</param>
        Task UpdateMeasureUnitAsync(MeasureUnitEntity measureUnitEntity);

        /// <summary>
        /// Метод обновляет статус единице измерения.
        /// </summary>
        /// <param name="measureUnitId">Id единицы измерения.</param>
        /// <param name="statusEnum">Новый статус единицы измерения.</param>
        Task ChangeStatusMeasureUnitAsync(int measureUnitId, DirectoryStatusEnum statusEnum);

        /// <summary>
        /// Метод удаляет единицу измерения.
        /// </summary>
        /// <param name="measureUnitId">Id единицы измерения.</param>
        Task RemoveMeasureUnitAsync(int measureUnitId);
    }
}
