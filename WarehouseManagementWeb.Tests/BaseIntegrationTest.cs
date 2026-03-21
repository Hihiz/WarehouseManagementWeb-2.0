using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WarehouseManagementWeb.Infrastructure.Data;
using WarehouseManagementWeb.Infrastructure.Repositories;

namespace WarehouseManagementWeb.Tests
{
    /// <summary>
    /// Базовый класс интеграционных тестов.
    /// </summary>
    public class BaseIntegrationTest 
        //: IAsyncLifetime
    {
        private readonly IConfiguration appConfiguration;

        protected internal readonly ClientRepository clientRepository;
        protected internal readonly DocumentReceiptRepository resourceReceiptRepository;
        protected internal readonly DocumentShipmentRepository documentShipmentRepository;
        protected internal readonly MeasureUnitRepository measureUnitRepository;
        protected internal readonly ResourceRepository resourceRepository;
        protected internal readonly ApplicationDbContext applicationDbContext;
        
        //private NpgsqlConnection _connection;
        //private Respawner _respawner;

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
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            clientRepository = new ClientRepository(applicationDbContext);
            resourceReceiptRepository = new DocumentReceiptRepository(applicationDbContext);
            documentShipmentRepository = new DocumentShipmentRepository(applicationDbContext);
            measureUnitRepository = new MeasureUnitRepository(applicationDbContext);
            resourceRepository = new ResourceRepository(applicationDbContext);

        }

        //public async Task InitializeAsync()
        //{
        //    if (_connection == null)
        //    {
        //        _connection = new NpgsqlConnection(
        //            appConfiguration.GetConnectionString("DefaultConnection"));

        //        await _connection.OpenAsync();
        //    }

        //    if (_respawner == null)
        //    {
        //        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        //        {
        //            DbAdapter = DbAdapter.Postgres,
        //            SchemasToInclude = new[] { "warehouse", "directory" },

        //            TablesToIgnore = new Table[]
        //            {
        //                new Table("directory", "measure_units"),
        //                new Table("directory", "resources"),
        //                new Table("directory", "clients"),
        //                new Table("warehouse", "balances"),
        //                new Table("warehouse", "balances"),
        //            }
        //        });
        //    }

        //    await _respawner.ResetAsync(_connection);
        //}

        //public async Task DisposeAsync()
        //{
        //    await applicationDbContext.DisposeAsync();

        //    if (_connection != null)
        //        await _connection.DisposeAsync();
        //}
    }
}