namespace WarehouseManagementWeb.Application.Dto.Input.MeasureUnit
{
    /// <summary>
    /// Класс входной модели создания единицы измерения.
    /// </summary>
    public class CreateMeasureUnitInput
    {
        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string? Title { get; set; }
    }
}
