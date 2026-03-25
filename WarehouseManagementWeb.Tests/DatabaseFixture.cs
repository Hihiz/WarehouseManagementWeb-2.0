using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;
using WarehouseManagementWeb.Infrastructure.Data;

namespace WarehouseManagementWeb.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:15-alpine")
            .WithDatabase("warehouse_container_db_tests")
            .WithUsername("test_user")
            .WithPassword("test_password")
            .Build();

        private NpgsqlConnection _dbConnection = null!;
        private Respawner _respawner = null!;

        public string ConnectionString => _dbContainer.GetConnectionString();

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(ConnectionString)
                .Options;
            await using var context = new ApplicationDbContext(options);
            await context.Database.MigrateAsync();

            _dbConnection = new NpgsqlConnection(ConnectionString);
            await _dbConnection.OpenAsync();

            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = new[] { "warehouse", "directory" },
            });
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public async Task DisposeAsync()
        {
            await _dbConnection.DisposeAsync();

            await _dbContainer.DisposeAsync();
        }
    }
}
