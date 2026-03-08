namespace WarehouseManagementWeb.Application.Dto.Input.Client
{
    /// <summary>
    /// Класс входной модели редактирования клиента.
    /// </summary>
    public class UpdateClientInput
    {
        /// <summary>
        /// Id клиента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование клиента.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Адрес клиента.
        /// </summary>
        public string? Address { get; set; }
    }
}
