using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WarehouseManagementWeb.Infrastructure.Data;
using WarehouseManagementWeb.Infrastructure.Identity;
using WarehouseManagementWeb.Infrastructure.Interfaces;
using WarehouseManagementWeb.Infrastructure.Services;

namespace WarehouseManagementWeb.Infrastructure
{
    /// <summary>
    /// Класс регистрирует зависимости в контейнере.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Метод добавляет инициализацию сервисов слоя Infrastructure в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Регистрация и получение зависимостей.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <returns>Коллекция зарегистрированных сервисов.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            IdentityInit(services);
            AuthenticationInit(services, configuration);
            ServicesInit(services);

            return services;
        }

        /// <summary>
        /// Метод регистрирует Identity сервисы.
        /// </summary>
        /// <param name="services">Регистрация зависимостей.</param>
        private static void IdentityInit(IServiceCollection services)
        {

            // TODO: Временная настройка.
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        /// <summary>
        /// Метод регистрирует сервисы аутентификации.
        /// </summary>
        /// <param name="services">Регистрация зависимостей.</param>
        private static void AuthenticationInit(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               // Настройка токена.
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"]!,
                       ValidAudience = configuration["Jwt:Audience"]!,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                           configuration["Jwt:SecretKey"]!)),
                       ClockSkew = TimeSpan.Zero
                   };
               });

            // Настройка авторизации
            services.AddAuthorization(options => options.DefaultPolicy =
                new AuthorizationPolicyBuilder
                    (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
        }

        /// <summary>
        /// Метод регистрирует сервисы.
        /// </summary>
        /// <param name="services">Регистрация зависимостей.</param>
        private static void ServicesInit(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
