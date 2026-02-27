using WarehouseManagementWeb.Infrastructure.Identity.Models;

namespace WarehouseManagementWeb.Infrastructure.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса токенов пользователей.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Метод обновляет токены пользователя (accessToken и refreshToken).
        /// </summary>
        /// <param name="tokenInput">Входная модель.</param>
        /// <returns>Выходная модель.</returns>
        Task<TokenOutput> RefreshTokenAsync(TokenInput tokenInput);

        /// <summary>
        /// Метод сбрасывает refreshToken пользователя по Email.
        /// </summary>
        /// <param name="userEmail">Email пользователя.</param>
        Task RevokeRefreshTokenUserByEmailAsync(string userEmail);    
    }
}
