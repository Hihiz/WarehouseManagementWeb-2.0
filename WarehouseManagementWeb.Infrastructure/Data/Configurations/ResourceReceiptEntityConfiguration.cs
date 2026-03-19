using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности ресурса поступления.
    /// </summary>
    public class ResourceReceiptEntityConfiguration : IEntityTypeConfiguration<ResourceReceiptEntity>
    {
        public void Configure(EntityTypeBuilder<ResourceReceiptEntity> entity)
        {
            entity.ToTable("resource_receipts", "warehouse");

            entity.ToTable(rr => rr.HasComment("Таблица ресурсов поступлений."));

            entity.Property(rr => rr.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(rr => rr.DocumentReceiptId)
                .HasColumnName("document_receipt_id")
                .HasComment("Id документа поступления.")
                .IsRequired();

            entity.Property(rr => rr.ResourceId)
                .HasColumnName("resource_id")
                .HasComment("Id ресурса.")
            .IsRequired();

            entity.Property(rr => rr.MeasureUnitId)
              .HasColumnName("measure_unit_id")
              .HasComment("Id единицы измерения.")
            .IsRequired();

            entity.Property(rr => rr.Quantity)
              .HasColumnName("quantity")
              .HasComment("Количество ресурса поступления.")
                .IsRequired();

            entity.HasIndex(rr => new { rr.DocumentReceiptId, rr.ResourceId, rr.MeasureUnitId })
              .IsUnique();

            entity.HasOne(rr => rr.DocumentReceiptEntity)
                .WithMany(dr => dr.ResourceReceiptEntities)
                .HasForeignKey(rr => rr.DocumentReceiptId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(rr => rr.ResourceEntity)
               .WithMany(r => r.ResourceReceiptEntities)
               .HasForeignKey(rr => rr.ResourceId)
               .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(rr => rr.MeasureUnitEntity)
              .WithMany(mu => mu.ResourceReceiptEntities)
              .HasForeignKey(rr => rr.MeasureUnitId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}