using Bogus;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Infrastructure.Data;
using WarehouseManagementWeb.Infrastructure.Repositories;

namespace WarehouseManagementWeb.Tests
{
    /// <summary>
    /// Базовый класс интеграционных тестов.
    /// </summary>
    [Collection("Database collection")]
    public class BaseIntegrationTest : IAsyncLifetime, IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        protected internal readonly ClientRepository clientRepository;
        protected internal readonly DocumentReceiptRepository resourceReceiptRepository;
        protected internal readonly DocumentShipmentRepository documentShipmentRepository;
        protected internal readonly MeasureUnitRepository measureUnitRepository;
        protected internal readonly ResourceRepository resourceRepository;
        protected internal readonly ApplicationDbContext applicationDbContext;
        protected internal Faker faker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseIntegrationTest(DatabaseFixture fixture)
        {
            _fixture = fixture;

            faker = new Faker("ru");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(_fixture.ConnectionString);

            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            clientRepository = new ClientRepository(applicationDbContext);
            resourceReceiptRepository = new DocumentReceiptRepository(applicationDbContext);
            documentShipmentRepository = new DocumentShipmentRepository(applicationDbContext);
            measureUnitRepository = new MeasureUnitRepository(applicationDbContext);
            resourceRepository = new ResourceRepository(applicationDbContext);
        }

        public async Task InitializeAsync()
        {
            await _fixture.ResetDatabaseAsync();
        }

        public async Task DisposeAsync()
        {
            await applicationDbContext.DisposeAsync();
        }
    }
}