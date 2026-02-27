namespace WarehouseManagementWeb.Infrastructure.Identity.Models
{
    /// <summary>
    /// Класс входной модели токенов пользователя.
    /// </summary>
    public class TokenInput
    {
        /// <summary>
        /// Токен доступа пользователя.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Токен обновления пользователя.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
