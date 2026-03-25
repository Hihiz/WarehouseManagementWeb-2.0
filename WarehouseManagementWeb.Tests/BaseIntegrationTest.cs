using Bogus;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;
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

        protected internal async Task<ClientEntity> SeedClientAsync(string? name = null)
        {
            var client = new ClientEntity
            {
                Name = name ?? faker.Company.CompanyName(),
                Address = faker.Address.FullAddress()
            };
            await clientRepository.CreateClientAsync(client);

            return client;
        }

        protected internal async Task<ResourceEntity> SeedResourceAsync(string? title = null)
        {
            var resource = new ResourceEntity { Title = title ?? faker.Commerce.ProductName() };

            await resourceRepository.CreateResourceAsync(resource);

            return resource;
        }

        protected internal async Task<MeasureUnitEntity> SeedMeasureUnitAsync(string? title = null)
        {
            var unit = new MeasureUnitEntity { Title = title ?? faker.Commerce.ProductName() };
            await measureUnitRepository.CreateMeasureUnitAsync(unit);

            return unit;
        }

        protected internal async Task<BalanceEntity> SeedBalancesAsync(ResourceEntity resource, MeasureUnitEntity unit,
            int quantity)
        {
            var balance = new BalanceEntity
            {
                ResourceId = resource.Id,
                MeasureUnitId = unit.Id,
                Quantity = quantity
            };

            await applicationDbContext.Balances.AddAsync(balance);
            await applicationDbContext.SaveChangesAsync();

            return balance;
        }
    }
}