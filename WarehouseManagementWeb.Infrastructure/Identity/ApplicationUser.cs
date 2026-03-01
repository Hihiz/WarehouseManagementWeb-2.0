using Microsoft.AspNetCore.Identity;

namespace WarehouseManagementWeb.Infrastructure.Identity
{
    /// <summary>
    /// Класс пользователя для авторизации.
    /// </summary>
    public class ApplicationUser : IdentityUser<long>
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        public DateTime RegisteredIn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Токен обновления, для получения нового AccessToken.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Время жизни токена обновления.
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}