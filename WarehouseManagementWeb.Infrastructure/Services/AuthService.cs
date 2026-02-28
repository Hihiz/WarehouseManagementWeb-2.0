using WarehouseManagementWeb.Infrastructure.Identity.Models;
using WarehouseManagementWeb.Infrastructure.Interfaces;

namespace WarehouseManagementWeb.Infrastructure.Services
{
    /// <summary>
    /// Класс реализует методы сервиса аутентификации пользователей.
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <inheritdoc />
        public async Task<UserSignUpOutput> SignUpAsync(UserSignUpInput userSignUpInput)
        {
            
        }

        /// <inheritdoc />
        public async Task<UserSignInOutput> SignInAsync(UserSignInInput userSignInInput)
        {
            
        }

        /// <inheritdoc />
        public async Task LogoutAsync(string userEmail)
        {
           
        }
    }
}
