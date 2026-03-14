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
                .HasComment("Id ресурса.");

            entity.Property(rr => rr.MeasureUnitId)
              .HasColumnName("measure_unit_id")
              .HasComment("Id единицы измерения.");

            entity.Property(rr => rr.Quantity)
              .HasColumnName("quantity")
              .HasComment("Количество ресурсов поступления.");

            entity.HasIndex(rr => new { rr.DocumentReceiptId, rr.ResourceId, rr.MeasureUnitId })
              .IsUnique();

            entity.HasOne(dr => dr.DocumentReceiptEntity)
                .WithMany(rr => rr.ResourceReceiptEntities)
                .HasForeignKey(dr => dr.DocumentReceiptId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(r => r.ResourceEntity)
               .WithMany(rr => rr.ResourceReceiptEntities)
               .HasForeignKey(r => r.ResourceId)
               .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(mu => mu.MeasureUnitEntity)
              .WithMany(rr => rr.ResourceReceiptEntities)
              .HasForeignKey(mu => mu.MeasureUnitId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}