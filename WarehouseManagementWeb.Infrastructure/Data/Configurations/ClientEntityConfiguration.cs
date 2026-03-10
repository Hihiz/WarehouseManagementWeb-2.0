using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagementWeb.Domain.Entities;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Класс конфигурации сущности клиента.
    /// </summary>
    public class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> entity)
        {
            entity.ToTable("clients", "directory");

            entity.ToTable(e => e.HasComment("Таблица клиентов."));

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("PK.")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasComment("Наименование клиента.")
                .IsRequired();

            entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasComment("Адрес клиента.")
                .IsRequired();

            entity.Property(e => e.ClientStatusEnum)
               .HasColumnName("status_enum")
               .HasColumnType("text")
               .HasConversion(
                   v => v.ToString().ToLower(),
                   v => Enum.Parse<DirectoryStatusEnum>(v, true))
               .HasDefaultValue(DirectoryStatusEnum.Active)
               .HasComment("Статус клиента в значении перечисления.")
               .IsRequired();

            entity.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
