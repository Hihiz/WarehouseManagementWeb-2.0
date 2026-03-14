using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Input.MeasureUnit
{
    /// <summary>
    /// Класс входной модели обновления статуса единицы измерения.
    /// </summary>
    public class ChangeStatusMeasureUnitInput
    {
        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int MeasureUnitId { get; set; }

        /// <summary>
        /// Статус единицы измерения.
        /// </summary>
        public DirectoryStatusEnum MeasureUnitStatusEnum { get; set; }
    }
}
