using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDocumentReceipts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "warehouse");

            migrationBuilder.CreateTable(
                name: "document_receipts",
                schema: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number_code = table.Column<string>(type: "text", nullable: false, comment: "Номер документа поступления."),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()", comment: "Дата документа поступления."),
                    client_id = table.Column<int>(type: "integer", nullable: false, comment: "Id клиента документа поступления.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_receipts", x => x.id);
                    table.ForeignKey(
                        name: "FK_document_receipts_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "directory",
                        principalTable: "clients",
                        principalColumn: "id");
                },
                comment: "Таблица документа поступления.");

            migrationBuilder.CreateIndex(
                name: "IX_document_receipts_client_id",
                schema: "warehouse",
                table: "document_receipts",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_receipts_number_code",
                schema: "warehouse",
                table: "document_receipts",
                column: "number_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_receipts",
                schema: "warehouse");
        }
    }
}
