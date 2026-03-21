using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности остатков ресурсов.
    /// </summary>
    public class BalanceEntityConfiguration : IEntityTypeConfiguration<BalanceEntity>
    {
        public void Configure(EntityTypeBuilder<BalanceEntity> entity)
        {
            entity.ToTable("balances", "warehouse");

            entity.ToTable(e => e.HasComment("Таблица остатков ресурсов на складе."));

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(e => e.ResourceId)
                .HasColumnName("resource_id")
                .HasComment("Id ресурса.")
                .IsRequired();

            entity.Property(e => e.MeasureUnitId)
              .HasColumnName("measure_unit_id")
              .HasComment("Id единицы измерения.")
              .IsRequired();

            entity.Property(e => e.Quantity)
            .HasColumnName("quantity")
            .HasComment("Количество ресурса.")
            .IsRequired();

            entity.HasIndex(b => new { b.ResourceId, b.MeasureUnitId })
                .IsUnique();

            entity.HasCheckConstraint("ck_balances_quantity_not_negative", "quantity >= 0");

            entity.HasOne(b => b.ResourceEntity)
                    .WithMany(r => r.BalanceEntities)
                    .HasForeignKey(b => b.ResourceId)
                    .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(b => b.MeasureUnitEntity)
                   .WithMany(m => m.BalanceEntities)
                   .HasForeignKey(b => b.MeasureUnitId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
