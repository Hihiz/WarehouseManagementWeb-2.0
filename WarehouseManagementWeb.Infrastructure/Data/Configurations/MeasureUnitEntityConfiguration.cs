using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности единицы измерения.
    /// </summary>
    public class MeasureUnitEntityConfiguration : IEntityTypeConfiguration<MeasureUnitEntity>
    {
        public void Configure(EntityTypeBuilder<MeasureUnitEntity> entity)
        {
            entity.ToTable("measure_units", "directory");

            entity.ToTable(e => e.HasComment("Таблица единиц измерений."));

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasComment("Наименование единицы измерения.")
                .IsRequired();

            entity.Property(e => e.MeasureUnitStatusEnum)
                .HasColumnName("status_enum")
                .HasColumnType("text")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => Enum.Parse<DirectoryStatusEnum>(v, true))
                .HasDefaultValue(DirectoryStatusEnum.Active)
                .HasComment("Статус единицы измерения в значении перечисления.")
                .IsRequired();

            entity.HasIndex(r => r.Title)
                .IsUnique();
        }
    }
}
