using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementWeb.Application.Interfaces.Services.Client;
using WarehouseManagementWeb.Application.Services.Client;

namespace WarehouseManagementWeb.Application
{
    /// <summary>
    /// Класс регистрирует зависимости в контейнере.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Метод добавляет инициализацию сервисов слоя Application в коллекцию сервисов. 
        /// </summary>
        /// <param name="services">Регистрация и получение зависимостей.</param>
        /// <returns>Коллекция зарегистрированных сервисов.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            ServicesInit(services);

            return services;
        }

        /// <summary>
        /// Метод регистрирует сервисы.
        /// </summary>
        /// <param name="services">Регистрация зависимостей.</param>
        private static void ServicesInit(IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
        }
    }
}
