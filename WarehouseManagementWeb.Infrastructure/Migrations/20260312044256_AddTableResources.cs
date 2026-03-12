using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "resources",
                schema: "directory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Наименование ресурса."),
                    status_enum = table.Column<string>(type: "text", nullable: false, defaultValue: "active", comment: "Статус ресурса в значении перечисления.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resources", x => x.id);
                },
                comment: "Таблица ресурсов.");

            migrationBuilder.CreateIndex(
                name: "IX_resources_title",
                schema: "directory",
                table: "resources",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resources",
                schema: "directory");
        }
    }
}
