using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности ресурса отгрузок.
    /// </summary>
    public class ResourceShipmentEntityConfiguration : IEntityTypeConfiguration<ResourceShipmentEntity>
    {
        public void Configure(EntityTypeBuilder<ResourceShipmentEntity> entity)
        {
            entity.ToTable("resource_shipments", "warehouse");

            entity.ToTable(rs => rs.HasComment("Таблица ресурсов отгрузок."));

            entity.Property(rs => rs.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(rs => rs.DocumentShipmentId)
                .HasColumnName("document_shipment_id")
                .HasComment("Id документа отгрузки.")
                .IsRequired();

            entity.Property(rs => rs.ResourceId)
               .HasColumnName("resource_id")
               .HasComment("Id ресурса.")
               .IsRequired();

            entity.Property(rs => rs.MeasureUnitId)
               .HasColumnName("measure_unit_id")
               .HasComment("Id единицы измерения.")
               .IsRequired();

            entity.Property(rs => rs.Quantity)
            .HasColumnName("quantity")
            .HasComment("Количество ресурса отгрузки.")
            .IsRequired();

            entity.HasIndex(rs => new { rs.DocumentShipmentId, rs.ResourceId, rs.MeasureUnitId })
                .IsUnique();

            entity.HasOne(rs => rs.DocumentShipmentEntity)
                .WithMany(ds => ds.ResourceShipmentEntities)
                .HasForeignKey(rs => rs.DocumentShipmentId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(rs => rs.ResourceEntity)
              .WithMany(r => r.ResourceShipmentEntities)
              .HasForeignKey(rs => rs.ResourceId)
              .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(rs => rs.MeasureUnitEntity)
              .WithMany(mu => mu.ResourceShipmentEntities)
              .HasForeignKey(rs => rs.MeasureUnitId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
