using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Infrastructure.Identity;

namespace WarehouseManagementWeb.Infrastructure.Data
{
    /// <summary>
    /// Класс контекста Ef Core.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<ResourceEntity> Resources { get; set; }

        public DbSet<MeasureUnitEntity> MeasureUnits { get; set; }

        public DbSet<DocumentReceiptEntity> DocumentReceipts { get; set; }

        public DbSet<ResourceReceiptEntity> ResourceReceipts { get; set; }

        public DbSet<DocumentShipmentEntity> DocumentShipments { get; set; }

        public DbSet<ResourceShipmentEntity> ResourceShipments { get; set; }

        public DbSet<BalanceEntity> Balances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
