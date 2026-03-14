namespace WarehouseManagementWeb.Application.Dto.Output.MeasureUnit
{
    /// <summary>
    /// Класс выходной модели единиц измерений разделенных по статусам.
    /// </summary>
    public class MeasureUnitListByStatusOutput
    {
        /// <summary>
        /// Список активных единиц измерения.
        /// </summary>
        public IEnumerable<MeasureUnitOutput>? ActiveMeasureUnits { get; set; }

        /// <summary>
        /// Список единиц измерения находящихся в архиве.
        /// </summary>
        public IEnumerable<MeasureUnitOutput>? ArchivedMeasureUnits { get; set; }
    }
}
