using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using WarehouseManagementWeb.Infrastructure.Data;
using WarehouseManagementWeb.Infrastructure.Repositories;

namespace WarehouseManagementWeb.Tests
{
    /// <summary>
    /// Базовый класс интеграционных тестов.
    /// </summary>
    public class BaseIntegrationTest : IDisposable
    {
        private readonly IConfiguration appConfiguration;
        private readonly IDbContextTransaction _transaction;

        protected internal readonly ClientRepository clientRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseIntegrationTest()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            appConfiguration = builder.Build();


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection"));
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            clientRepository = new ClientRepository(applicationDbContext);

            _transaction = applicationDbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            // Откатываем изменения после тест метода.
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
