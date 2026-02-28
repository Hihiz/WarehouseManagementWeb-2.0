using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Security.Claims;
using WarehouseManagementWeb.Infrastructure.Data;
using WarehouseManagementWeb.Infrastructure.Helpers;
using WarehouseManagementWeb.Infrastructure.Identity;
using WarehouseManagementWeb.Infrastructure.Identity.Models;
using WarehouseManagementWeb.Infrastructure.Interfaces;

namespace WarehouseManagementWeb.Infrastructure.Services
{
    /// <summary>
    /// Класс реализует методы сервиса аутентификации пользователей.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="logger">Логгер.</param>
        /// <param name="db">Класс контекста Ef.</param>
        public AuthService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _db = db;
        }

        /// <inheritdoc />
        public async Task<UserSignUpOutput> SignUpAsync(UserSignUpInput userSignUpInput)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                if (userSignUpInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные пользователя.");
                }

                if (userSignUpInput.Password != userSignUpInput.PasswordConfirm)
                {
                    throw new InvalidOperationException("Пароли не совпадают.");
                }

                ApplicationUser? findUser = await _userManager.FindByEmailAsync(userSignUpInput.Email!);

                if (findUser is not null)
                {
                    throw new InvalidOperationException(
                        $"Пользователь с Email: {userSignUpInput.Email} уже зарегистрирован в системе.");
                }

                ApplicationUser user = new ApplicationUser
                {
                    FirstName = userSignUpInput.FirstName!,
                    LastName = userSignUpInput.LastName!,
                    Email = userSignUpInput.Email,
                    UserName = userSignUpInput.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                // Создаем пользователя.
                IdentityResult createUser = await _userManager.CreateAsync(user, userSignUpInput.Password!);

                if (!createUser.Succeeded)
                {
                    throw new InvalidOperationException($"Ошибка при создании пользователя {userSignUpInput.Email}.");
                }

                // Добавляем роль новому пользователю.           
                await _userManager.AddToRoleAsync(user, "User");

                await transaction.CommitAsync();

                UserSignUpOutput result = new UserSignUpOutput
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                return result;
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError(ex, ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<UserSignInOutput> SignInAsync(UserSignInInput userSignInInput)
        {
            try
            {
                if (userSignInInput is null)
                {
                    throw new InvalidOperationException("Недопустимые данные пользователя.");
                }

                ApplicationUser? user = await _userManager.FindByEmailAsync(userSignInInput.Email!);

                if (user is null)
                {
                    throw new InvalidOperationException(
                        $"Пользователь с почтой {userSignInInput.Email} не существует в системе.");
                }

                // Проверяем пароль пользователя.
                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, userSignInInput.Password!);

                if (!isPasswordValid)
                {
                    throw new InvalidOperationException("Не удалось выполнить вход. " +
                        "Проверьте корректность учётных данных.");
                }

                // Получаем роли пользователя.
                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                // Добавляем роли пользователя в claims.
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Создаем токен доступа.
                string accessToken = _configuration.GenerateAccessToken(authClaims);

                // Создаем токен обновления для пользователя.
                user.RefreshToken = _configuration.GenerateRefreshToken();

                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(_configuration.GetSection(
                    "Jwt:RefreshTokenValidityInMinutes").Get<int>());

                await _userManager.UpdateAsync(user);

                UserSignInOutput result = new UserSignInOutput
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    AccessToken = accessToken,
                    RefreshToken = user.RefreshToken
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
        public async Task LogoutAsync(string userEmail)
        {
           
        }
    }
}
