namespace WarehouseManagementWeb.Infrastructure.Identity.Models
{
    /// <summary>
    /// Класс входной модели прохождения аутентификации пользователя.
    /// </summary>
    public class UserSignInInput
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string? Password { get; set; }
    }
}
