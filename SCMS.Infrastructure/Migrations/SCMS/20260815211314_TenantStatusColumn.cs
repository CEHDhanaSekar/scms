using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scms.Infrastructure.Migrations.SCMS
{
    /// <inheritdoc />
    public partial class TenantStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "tenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "tenants",
                keyColumn: "id",
                keyValue: new Guid("d4444444-0000-0000-0000-000000000001"),
                column: "status",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "tenants");
        }
    }
}
