using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableBalances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "balances",
                schema: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    resource_id = table.Column<int>(type: "integer", nullable: false, comment: "Id ресурса."),
                    measure_unit_id = table.Column<int>(type: "integer", nullable: false, comment: "Id единицы измерения."),
                    quantity = table.Column<int>(type: "integer", nullable: false, comment: "Количество ресурса.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_balances", x => x.id);
                    table.CheckConstraint("ck_balances_quantity_not_negative", "quantity >= 0");
                    table.ForeignKey(
                        name: "FK_balances_measure_units_measure_unit_id",
                        column: x => x.measure_unit_id,
                        principalSchema: "directory",
                        principalTable: "measure_units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_balances_resources_resource_id",
                        column: x => x.resource_id,
                        principalSchema: "directory",
                        principalTable: "resources",
                        principalColumn: "id");
                },
                comment: "Таблица остатков ресурсов на складе.");

            migrationBuilder.CreateIndex(
                name: "IX_balances_measure_unit_id",
                schema: "warehouse",
                table: "balances",
                column: "measure_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_balances_resource_id_measure_unit_id",
                schema: "warehouse",
                table: "balances",
                columns: new[] { "resource_id", "measure_unit_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balances",
                schema: "warehouse");
        }
    }
}
