
using WarehouseManagementWeb.Infrastructure.Identity.Models;

namespace WarehouseManagementWeb.Infrastructure.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса аутентификации пользователей.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Метод регистрирует нового пользователя.
        /// </summary>
        /// <param name="userSignUpInput">Входная модель.</param>
        /// <returns>Выходная модель.</returns>
        Task<UserSignUpOutput> SignUpAsync(UserSignUpInput userSignUpInput);

        /// <summary>
        /// Метод аутентифицирует пользователя.
        /// </summary>
        /// <param name="userSignInInput">Входная модель.</param>
        /// <returns>Выходная модель.</returns>
        Task<UserSignInOutput> SignInAsync(UserSignInInput userSignInInput);

        /// <summary>
        /// Метод выходит из аккаунта пользователя.
        /// </summary>
        /// <param name="userEmail">Email пользователя.</param>
        Task LogoutAsync(string userEmail);
    }
}
