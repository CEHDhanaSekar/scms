using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scms.Infrastructure.Migrations.SCMS
{
    /// <inheritdoc />
    public partial class OwnerTenantAdminUserConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "owner_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    mobile = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_owner_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "owner_refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    replaced_by_token = table.Column<string>(type: "text", nullable: true),
                    created_by_ip = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_owner_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_owner_refresh_tokens_owner_users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "owner_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "owner_users",
                columns: new[] { "id", "created_at", "created_by", "deleted_at", "deleted_by", "email", "is_active", "is_deleted", "last_login_at", "mobile", "name", "password_hash", "updated_at", "updated_by", "username" },
                values: new object[] { new Guid("b2222222-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, "admin@scms.com", true, false, null, "9876543210", "Super Admin", "ChQeKDI8RlBaZG54goyWoA==.hlD+fFig3GxPS0zzbBxxENgmlvr4JL+IBJttzay3JG8=", null, null, "admin" });

            migrationBuilder.CreateIndex(
                name: "ix_owner_refresh_tokens_owner_user_id",
                table: "owner_refresh_tokens",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_owner_refresh_tokens_token",
                table: "owner_refresh_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_owner_users_email",
                table: "owner_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_owner_users_username",
                table: "owner_users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "owner_refresh_tokens");

            migrationBuilder.DropTable(
                name: "owner_users");
        }
    }
}
