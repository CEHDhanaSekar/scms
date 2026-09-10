using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace scms.Infrastructure.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class MasterValuesEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "employees");

            migrationBuilder.AddColumn<Guid>(
                name: "type_id",
                table: "employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "master_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_master_values", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "master_values",
                columns: new[] { "id", "description", "display_name", "is_active", "key", "type" },
                values: new object[,]
                {
                    { new Guid("e5555555-0000-0000-0000-000000000001"), "Doctor", "Doctor", true, "Doctor", "EmployeeType" },
                    { new Guid("e5555555-0000-0000-0000-000000000002"), "Nurse", "Nurse", true, "Nurse", "EmployeeType" },
                    { new Guid("e5555555-0000-0000-0000-000000000003"), "Receptionist", "Receptionist", true, "Receptionist", "EmployeeType" },
                    { new Guid("e5555555-0000-0000-0000-000000000004"), "Administrator", "Admin", true, "Admin", "EmployeeType" },
                    { new Guid("e5555555-0000-0000-0000-000000000005"), "Other", "Other", true, "Other", "EmployeeType" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_type_id",
                table: "employees",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_master_values_type_id",
                table: "employees",
                column: "type_id",
                principalTable: "master_values",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_master_values_type_id",
                table: "employees");

            migrationBuilder.DropTable(
                name: "master_values");

            migrationBuilder.DropIndex(
                name: "ix_employees_type_id",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "employees");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
