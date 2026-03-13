using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableMeasureUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "measure_units",
                schema: "directory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Наименование единицы измерения."),
                    status_enum = table.Column<string>(type: "text", nullable: false, defaultValue: "active", comment: "Статус единицы измерения в значении перечисления.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measure_units", x => x.id);
                },
                comment: "Таблица единиц измерений.");

            migrationBuilder.CreateIndex(
                name: "IX_measure_units_title",
                schema: "directory",
                table: "measure_units",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "measure_units",
                schema: "directory");
        }
    }
}
