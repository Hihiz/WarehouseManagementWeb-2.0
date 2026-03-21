using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDocumentShipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: false,
                comment: "Количество ресурса поступления.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Количество ресурсов поступления.");

            migrationBuilder.CreateTable(
                name: "document_shipments",
                schema: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number_code = table.Column<string>(type: "text", nullable: false, comment: "Номер документа отгрузки."),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата документа отгрузки."),
                    client_id = table.Column<int>(type: "integer", nullable: false, comment: "Id клиента документа отгрузки."),
                    status_enum = table.Column<string>(type: "text", nullable: false, defaultValue: "active", comment: "Статус документа отгрузки в значении перечисления.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_shipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_document_shipments_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "directory",
                        principalTable: "clients",
                        principalColumn: "id");
                },
                comment: "Таблица документов отгрузок.");

            migrationBuilder.CreateTable(
                name: "resource_shipments",
                schema: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    document_shipment_id = table.Column<int>(type: "integer", nullable: false, comment: "Id документа отгрузки."),
                    resource_id = table.Column<int>(type: "integer", nullable: false, comment: "Id ресурса."),
                    measure_unit_id = table.Column<int>(type: "integer", nullable: false, comment: "Id единицы измерения."),
                    quantity = table.Column<int>(type: "integer", nullable: false, comment: "Количество ресурса отгрузки.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_shipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_resource_shipments_document_shipments_document_shipment_id",
                        column: x => x.document_shipment_id,
                        principalSchema: "warehouse",
                        principalTable: "document_shipments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_resource_shipments_measure_units_measure_unit_id",
                        column: x => x.measure_unit_id,
                        principalSchema: "directory",
                        principalTable: "measure_units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_resource_shipments_resources_resource_id",
                        column: x => x.resource_id,
                        principalSchema: "directory",
                        principalTable: "resources",
                        principalColumn: "id");
                },
                comment: "Таблица ресурсов отгрузок.");

            migrationBuilder.CreateIndex(
                name: "IX_document_shipments_client_id",
                schema: "warehouse",
                table: "document_shipments",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_shipments_number_code",
                schema: "warehouse",
                table: "document_shipments",
                column: "number_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_resource_shipments_document_shipment_id_resource_id_measure~",
                schema: "warehouse",
                table: "resource_shipments",
                columns: new[] { "document_shipment_id", "resource_id", "measure_unit_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_resource_shipments_measure_unit_id",
                schema: "warehouse",
                table: "resource_shipments",
                column: "measure_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_resource_shipments_resource_id",
                schema: "warehouse",
                table: "resource_shipments",
                column: "resource_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resource_shipments",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "document_shipments",
                schema: "warehouse");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: false,
                comment: "Количество ресурсов поступления.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Количество ресурса поступления.");
        }
    }
}
