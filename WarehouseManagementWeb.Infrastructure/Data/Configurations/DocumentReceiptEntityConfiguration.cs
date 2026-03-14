using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности документа поступления.
    /// </summary>
    public class DocumentReceiptEntityConfiguration : IEntityTypeConfiguration<DocumentReceiptEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentReceiptEntity> entity)
        {
            entity.ToTable("document_receipts", "warehouse");

            entity.ToTable(dr => dr.HasComment("Таблица документов поступлений."));

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(dr => dr.NumberCode)
                .HasColumnName("number_code")
                .HasComment("Номер документа поступления.")
                .IsRequired();

            entity.Property(dr => dr.Date)
                .HasColumnName("date")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()")
                .HasComment("Дата документа поступления.")
                .IsRequired();

            entity.Property(dr => dr.ClientId)
               .HasColumnName("client_id")
               .HasComment("Id клиента документа поступления.")
               .IsRequired();

            entity.HasIndex(dr => dr.NumberCode)
                .IsUnique();

            entity.HasOne(dr => dr.ClientEntity)
                .WithMany(dr => dr.DocumentReceiptEntities)
                .HasForeignKey(dr => dr.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
