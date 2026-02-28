using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using WarehouseManagementWeb.Infrastructure.Data;
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
        private readonly ILogger<AuthService> _logger;
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="tokenService">Сервисо токенов.</param>
        /// <param name="logger">Логгер.</param>
        /// <param name="db">Класс контекста Ef.</param>
        public AuthService(UserManager<ApplicationUser> userManager,     
            ILogger<AuthService> logger,
            ApplicationDbContext db)
        {
            _userManager = userManager;
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
            
        }

        /// <inheritdoc />
        public async Task LogoutAsync(string userEmail)
        {
           
        }
    }
}
