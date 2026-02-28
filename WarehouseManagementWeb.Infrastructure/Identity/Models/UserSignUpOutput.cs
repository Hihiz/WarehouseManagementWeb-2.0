namespace WarehouseManagementWeb.Infrastructure.Identity.Models
{
    /// <summary>
    /// Класс выходной модели регистрации пользователя.
    /// </summary>
    public class UserSignUpOutput
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
    }
}
