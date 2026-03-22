using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChageDefaultStatusShipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status_enum",
                schema: "warehouse",
                table: "document_shipments",
                type: "text",
                nullable: false,
                defaultValue: "inactive",
                comment: "Статус документа отгрузки в значении перечисления.",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "active",
                oldComment: "Статус документа отгрузки в значении перечисления.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status_enum",
                schema: "warehouse",
                table: "document_shipments",
                type: "text",
                nullable: false,
                defaultValue: "active",
                comment: "Статус документа отгрузки в значении перечисления.",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "inactive",
                oldComment: "Статус документа отгрузки в значении перечисления.");
        }
    }
}
