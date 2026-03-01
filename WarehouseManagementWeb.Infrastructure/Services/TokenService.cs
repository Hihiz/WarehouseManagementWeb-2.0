using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WarehouseManagementWeb.Infrastructure.Helpers;
using WarehouseManagementWeb.Infrastructure.Identity;
using WarehouseManagementWeb.Infrastructure.Identity.Models;
using WarehouseManagementWeb.Infrastructure.Interfaces;

namespace WarehouseManagementWeb.Infrastructure.Services
{
    /// <summary>
    /// Класс реализует методы сервиса токенов пользователей.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="logger">Логгер.</param>
        public TokenService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<TokenService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TokenOutput> RefreshTokenAsync(TokenInput tokenInput)
        {
            try
            {
                if (tokenInput is null)
                {
                    throw new InvalidOperationException("Недопустимый токен доступа или токен обновления.");
                }

                ClaimsPrincipal claimsPrincipal = _configuration.GetPrincipalFromExpiredToken(tokenInput.AccessToken);

                if (claimsPrincipal is null)
                {
                    throw new InvalidOperationException("Недопустимый токен доступа или токен обновления.");
                }

                string userName = claimsPrincipal.Identity!.Name!;

                // Получаем пользователя по userName.
                ApplicationUser? user = await _userManager.FindByEmailAsync(userName);

                if (user is null || user.RefreshToken != tokenInput.RefreshToken
                    || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    throw new InvalidOperationException($"Недопустимый токен доступа или токен обновления.");
                }

                // Получаем новый токен доступа.
                string newAccessToken = _configuration.GenerateAccessToken(claimsPrincipal.Claims.ToList());

                // Получаем новый токен обновления.
                string newRefreshToken = _configuration.GenerateRefreshToken();

                // Обновляем токен обновления пользователю.
                user.RefreshToken = newRefreshToken;

                // Обновляем время жизни токена обновления.
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(_configuration.GetSection(
                    "Jwt:RefreshTokenValidityInMinutes").Get<int>());

                await _userManager.UpdateAsync(user);

                TokenOutput result = new TokenOutput
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task RevokeRefreshTokenUserByEmailAsync(string userEmail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    throw new InvalidOperationException($"Недопустимый email пользователя. UserEmail: {userEmail}.");
                }

                ApplicationUser? user = await _userManager.FindByEmailAsync(userEmail);

                if (user is null)
                {
                    throw new InvalidOperationException($"Ошибка получения пользователя. UserEmail: {userEmail}.");
                }

                // Обнуляем токен обновления.
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;

                await _userManager.UpdateAsync(user);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw;
            }
        }
    }
}
