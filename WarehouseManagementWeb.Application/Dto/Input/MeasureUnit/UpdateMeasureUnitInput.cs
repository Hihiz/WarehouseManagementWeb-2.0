namespace WarehouseManagementWeb.Application.Dto.Input.MeasureUnit
{
    /// <summary>
    /// Класс входной модели редактирования единицы измерения.
    /// </summary>
    public class UpdateMeasureUnitInput
    {
        /// <summary>
        /// Id единицы измерения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование единицы измерения.
        /// </summary>
        public string? Title { get; set; }
    }
}
