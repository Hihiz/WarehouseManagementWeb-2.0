namespace WarehouseManagementWeb.Application.Dto.Input.Resource
{
    /// <summary>
    /// Класс входной модели редактирования ресурса.
    /// </summary>
    public class UpdateResourceInput
    {
        /// <summary>
        /// Id ресурса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string? Title { get; set; }
    }
}
