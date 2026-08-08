using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scms.Infrastructure.Migrations.SCMS
{
    /// <inheritdoc />
    public partial class MasterTenantDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "plans",
                columns: new[] { "id", "billing_cycle", "created_at", "created_by", "deleted_at", "deleted_by", "is_active", "is_deleted", "max_employees", "max_users", "plan_name", "price_monthly", "price_yearly", "updated_at", "updated_by" },
                values: new object[] { new Guid("c3333333-0000-0000-0000-000000000001"), 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, true, false, 2147483647, 2147483647, "Master", 0m, 0m, null, null });

            migrationBuilder.InsertData(
                table: "tenants",
                columns: new[] { "id", "address_line1", "address_line2", "city", "contact_person_name", "country", "created_at", "created_by", "deleted_at", "deleted_by", "domain_url", "email", "is_active", "is_deleted", "logo_url", "mobile_phone", "name", "plan_id", "postal_code", "state", "tenant_code", "updated_at", "updated_by" },
                values: new object[] { new Guid("d4444444-0000-0000-0000-000000000001"), null, null, null, "Admin", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System", null, null, "http://localhost:4200", "admin@vktech.com", true, false, null, "0000000000", "VK Tech (Owner)", new Guid("c3333333-0000-0000-0000-000000000001"), null, null, "vktech", null, null });

            migrationBuilder.CreateIndex(
                name: "ix_tenants_domain_url",
                table: "tenants",
                column: "domain_url",
                unique: true,
                filter: "domain_url IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_tenants_tenant_code",
                table: "tenants",
                column: "tenant_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tenants_domain_url",
                table: "tenants");

            migrationBuilder.DropIndex(
                name: "ix_tenants_tenant_code",
                table: "tenants");

            migrationBuilder.DeleteData(
                table: "tenants",
                keyColumn: "id",
                keyValue: new Guid("d4444444-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "plans",
                keyColumn: "id",
                keyValue: new Guid("c3333333-0000-0000-0000-000000000001"));
        }
    }
}
