using WarehouseManagementWeb.Infrastructure.Identity.Models;
using WarehouseManagementWeb.Infrastructure.Interfaces;

namespace WarehouseManagementWeb.Infrastructure.Services
{
    /// <summary>
    /// Класс реализует методы сервиса токенов пользователей.
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <inheritdoc />
        public async Task<TokenOutput> RefreshTokenAsync(TokenInput tokenInput)
        {
           
        }

        /// <inheritdoc />
        public async Task RevokeRefreshTokenUserByEmailAsync(string userEmail)
        {
           
        }
    }
}
