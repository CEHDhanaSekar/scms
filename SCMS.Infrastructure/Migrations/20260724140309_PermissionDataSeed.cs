using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace scms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PermissionDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "description", "is_active", "permission_key", "permission_name" },
                values: new object[,]
                {
                    { new Guid("a1111111-0000-0000-0000-000000000001"), "View/read access to a module's data", true, "READ", "Read" },
                    { new Guid("a1111111-0000-0000-0000-000000000002"), "Create or update a module's data", true, "WRITE", "Write" },
                    { new Guid("a1111111-0000-0000-0000-000000000003"), "Delete/remove a module's data", true, "DELETE", "Delete" },
                    { new Guid("a1111111-0000-0000-0000-000000000004"), "Export a module's data (reports/CSV)", true, "EXPORT", "Export" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("a1111111-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("a1111111-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("a1111111-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("a1111111-0000-0000-0000-000000000004"));
        }
    }
}
