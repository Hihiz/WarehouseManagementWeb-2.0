using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности документа отгрузки.
    /// </summary>
    public class DocumentShipmentEntityConfiguration : IEntityTypeConfiguration<DocumentShipmentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentShipmentEntity> entity)
        {
            entity.ToTable("document_shipments", "warehouse");

            entity.ToTable(ds => ds.HasComment("Таблица документов отгрузок."));

            entity.Property(ds => ds.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(ds => ds.NumberCode)
                .HasColumnName("number_code")
                .HasComment("Номер документа отгрузки.")
                .IsRequired();

            entity.Property(ds => ds.Date)
                .HasColumnName("date")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()")
                .HasComment("Дата документа отгрузки.")
                .IsRequired();

            entity.Property(ds => ds.ClientId)
               .HasColumnName("client_id")
               .HasComment("Id клиента документа отгрузки.")
               .IsRequired();

            entity.Property(ds => ds.DocumentShipmentStatusEnum)
                .HasColumnName("status_enum")
                .HasColumnType("text")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => Enum.Parse<DocumentStatusEnum>(v, true))
                .HasDefaultValue(DocumentStatusEnum.Active)
                .HasComment("Статус документа отгрузки в значении перечисления.")
                .IsRequired();

            entity.HasIndex(ds => ds.NumberCode)
                .IsUnique();

            entity.HasOne(ds => ds.ClientEntity)
                .WithMany(c => c.DocumentShipmentEntities)
                .HasForeignKey(ds => ds.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
