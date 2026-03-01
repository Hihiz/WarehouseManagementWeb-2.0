namespace WarehouseManagementWeb.Infrastructure.Identity.Models
{
    /// <summary>
    /// Класс выходной модели прохождения аутентификации пользователя.
    /// </summary>
    public class UserSignInOutput
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string? Email { get; set; }

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
