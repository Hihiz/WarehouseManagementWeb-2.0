using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "resource_id",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Id ресурса.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Id ресурса.");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Количество ресурсов поступления.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Количество ресурсов поступления.");

            migrationBuilder.AlterColumn<int>(
                name: "measure_unit_id",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Id единицы измерения.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Id единицы измерения.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "resource_id",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: true,
                comment: "Id ресурса.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Id ресурса.");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: true,
                comment: "Количество ресурсов поступления.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Количество ресурсов поступления.");

            migrationBuilder.AlterColumn<int>(
                name: "measure_unit_id",
                schema: "warehouse",
                table: "resource_receipts",
                type: "integer",
                nullable: true,
                comment: "Id единицы измерения.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Id единицы измерения.");
        }
    }
}
