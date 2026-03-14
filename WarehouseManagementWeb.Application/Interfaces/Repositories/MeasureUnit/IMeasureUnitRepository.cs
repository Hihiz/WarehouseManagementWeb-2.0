using WarehouseManagementWeb.Application.Dto.Output.MeasureUnit;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Interfaces.Repositories.MeasureUnit
{
    /// <summary>
    /// Интерфейс репозитория единиц измерений.
    /// </summary>
    public interface IMeasureUnitRepository
    {
        /// <summary>
        /// Метод получает список единиц измерений.
        /// </summary>
        /// <returns>Список единиц измерений.</returns>
        Task<IEnumerable<MeasureUnitOutput>> GetMeasureUnitsAsync();

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
        /// Метод проверяет существование единицы измерения по ее наименованию.
        /// </summary>
        /// <param name="measureUnitTitle">Наименование единицы измерения.</param>
        /// <returns>Признак существования единицы измерения.</returns>
        Task<bool> CheckMeasureUnitExistsByTitleAsync(string measureUnitTitle);

        /// <summary>
        /// Метод проверяет существование единицы измерения по ее наименованию и Id единицы измерения.
        /// </summary>
        /// <param name="measureUnitId">Id единицы измерения.</param>
        /// <param name="measureUnitTitle">Наименование единицы измерения.</param>
        /// <returns>Признак существовании единицы измерения.</returns>
        Task<bool> CheckMeasureUnitExistsByIdAndTitleAsync(int measureUnitId, string measureUnitTitle);

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
