using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Application.Dto.Output.MeasureUnit
{
    /// <summary>
    /// Класс выходной модели единицы измерения.
    /// </summary>
    public class MeasureUnitOutput
    {
        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Статус единицы измерения в значении перечисления.
        /// </summary>
        public DirectoryStatusEnum MeasureUnitStatusEnum { get; set; }
    }
}
