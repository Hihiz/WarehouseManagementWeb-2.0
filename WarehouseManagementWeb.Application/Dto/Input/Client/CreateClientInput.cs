namespace WarehouseManagementWeb.Application.Dto.Input.Client
{
    /// <summary>
    /// Класс входной модели создания клиента.
    /// </summary>
    public class CreateClientInput
    {
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
