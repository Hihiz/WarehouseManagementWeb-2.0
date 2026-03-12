using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности ресурса.
    /// </summary>
    public class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceEntity>
    {
        public void Configure(EntityTypeBuilder<ResourceEntity> entity)
        {
            entity.ToTable("resources", "directory");

            entity.ToTable(e => e.HasComment("Таблица ресурсов."));

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasComment("Наименование ресурса.")
                .IsRequired();

            entity.Property(e => e.ResourceStatusEnum)
                .HasColumnName("status_enum")
                .HasColumnType("text")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => Enum.Parse<DirectoryStatusEnum>(v, true))
                .HasDefaultValue(DirectoryStatusEnum.Active)
                .HasComment("Статус ресурса в значении перечисления.")
                .IsRequired();

            entity.HasIndex(r => r.Title)
                .IsUnique();
        }
    }
}
