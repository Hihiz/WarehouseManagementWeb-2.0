using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WarehouseManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "directory");

            migrationBuilder.EnsureSchema(
                name: "warehouse");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    RegisteredIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "directory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Наименование клиента."),
                    address = table.Column<string>(type: "text", nullable: false, comment: "Адрес клиента."),
                    status_enum = table.Column<string>(type: "text", nullable: false, defaultValue: "active", comment: "Статус клиента в значении перечисления.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                },
                comment: "Таблица клиентов.");

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                comment: "Таблица документов поступлений.");

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
                comment: "Таблица ресурсов поступлений.");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_name",
                schema: "directory",
                table: "clients",
                column: "name",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_measure_units_title",
                schema: "directory",
                table: "measure_units",
                column: "title",
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "resource_receipts",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "document_receipts",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "measure_units",
                schema: "directory");

            migrationBuilder.DropTable(
                name: "resources",
                schema: "directory");

            migrationBuilder.DropTable(
                name: "clients",
                schema: "directory");
        }
    }
}
