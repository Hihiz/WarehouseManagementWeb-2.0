using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableResourceReceipts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "resource_receipts",
                schema: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    document_receipt_id = table.Column<int>(type: "integer", nullable: false, comment: "Id документа поступления."),
                    resource_id = table.Column<int>(type: "integer", nullable: true, comment: "Id ресурса."),
                    measure_unit_id = table.Column<int>(type: "integer", nullable: true, comment: "Id единицы измерения."),
                    quantity = table.Column<int>(type: "integer", nullable: true, comment: "Количество ресурсов поступления.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_receipts", x => x.id);
                    table.ForeignKey(
                        name: "FK_resource_receipts_document_receipts_document_receipt_id",
                        column: x => x.document_receipt_id,
                        principalSchema: "warehouse",
                        principalTable: "document_receipts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_resource_receipts_measure_units_measure_unit_id",
                        column: x => x.measure_unit_id,
                        principalSchema: "directory",
                        principalTable: "measure_units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_resource_receipts_resources_resource_id",
                        column: x => x.resource_id,
                        principalSchema: "directory",
                        principalTable: "resources",
                        principalColumn: "id");
                },
                comment: "Таблица ресурсов поступления.");

            migrationBuilder.CreateIndex(
                name: "IX_resource_receipts_document_receipt_id_resource_id_measure_u~",
                schema: "warehouse",
                table: "resource_receipts",
                columns: new[] { "document_receipt_id", "resource_id", "measure_unit_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_resource_receipts_measure_unit_id",
                schema: "warehouse",
                table: "resource_receipts",
                column: "measure_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_resource_receipts_resource_id",
                schema: "warehouse",
                table: "resource_receipts",
                column: "resource_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resource_receipts",
                schema: "warehouse");
        }
    }
}
