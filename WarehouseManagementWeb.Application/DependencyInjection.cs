using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementWeb.Application.Interfaces.Services.Balance;
using WarehouseManagementWeb.Application.Interfaces.Services.Client;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentReceipt;
using WarehouseManagementWeb.Application.Interfaces.Services.DocumentShipment;
using WarehouseManagementWeb.Application.Interfaces.Services.MeasureUnit;
using WarehouseManagementWeb.Application.Interfaces.Services.Resource;
using WarehouseManagementWeb.Application.Services.Balance;
using WarehouseManagementWeb.Application.Services.Client;
using WarehouseManagementWeb.Application.Services.DocumentReceipt;
using WarehouseManagementWeb.Application.Services.DocumentShipment;
using WarehouseManagementWeb.Application.Services.MeasureUnit;
using WarehouseManagementWeb.Application.Services.Resource;

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
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IMeasureUnitService, MeasureUnitService>();
            services.AddScoped<IDocumentReceiptService, DocumentReceiptService>();
            services.AddScoped<IDocumentShipmentService, DocumentShipmentService>();
            services.AddScoped<IBalanceService, BalanceService>();
        }
    }
}