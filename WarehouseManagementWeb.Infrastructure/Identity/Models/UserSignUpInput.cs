namespace WarehouseManagementWeb.Infrastructure.Identity.Models
{
    /// <summary>
    /// Класс входной модели регистрации пользователя.
    /// </summary>
    public class UserSignUpInput
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
        /// Пароль пользователя.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        public string? PasswordConfirm { get; set; }
    }
}
