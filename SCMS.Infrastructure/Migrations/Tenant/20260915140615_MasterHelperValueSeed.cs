using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace scms.Infrastructure.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class MasterHelperValueSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000001"),
                columns: new[] { "key", "type" },
                values: new object[] { "doctor", "EMPLOYEE_TYPE" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000002"),
                columns: new[] { "key", "type" },
                values: new object[] { "nurse", "EMPLOYEE_TYPE" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000003"),
                columns: new[] { "key", "type" },
                values: new object[] { "receptionist", "EMPLOYEE_TYPE" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000004"),
                columns: new[] { "key", "type" },
                values: new object[] { "admin", "EMPLOYEE_TYPE" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000005"),
                columns: new[] { "key", "type" },
                values: new object[] { "other", "EMPLOYEE_TYPE" });

            migrationBuilder.InsertData(
                table: "master_values",
                columns: new[] { "id", "description", "display_name", "is_active", "key", "type" },
                values: new object[,]
                {
                    { new Guid("e5555555-0002-0000-0000-000000000001"), "Walk In", "Walk In", true, "walk_in", "VISIT_MODE" },
                    { new Guid("e5555555-0002-0000-0000-000000000002"), "Scheduled", "Scheduled", true, "scheduled", "VISIT_MODE" },
                    { new Guid("e5555555-0003-0000-0000-000000000001"), "Male", "Male", true, "male", "GENDER" },
                    { new Guid("e5555555-0003-0000-0000-000000000002"), "Female", "Female", true, "female", "GENDER" },
                    { new Guid("e5555555-0003-0000-0000-000000000003"), "Other", "Other", true, "other", "GENDER" },
                    { new Guid("e5555555-0004-0000-0000-000000000001"), "A Positive", "A+", true, "a_pos", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000002"), "A Negative", "A-", true, "a_neg", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000003"), "B Positive", "B+", true, "b_pos", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000004"), "B Negative", "B-", true, "b_neg", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000005"), "O Positive", "O+", true, "o_pos", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000006"), "O Negative", "O-", true, "o_neg", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000007"), "AB Positive", "AB+", true, "ab_pos", "BLOOD_GROUP" },
                    { new Guid("e5555555-0004-0000-0000-000000000008"), "AB Negative", "AB-", true, "ab_neg", "BLOOD_GROUP" },
                    { new Guid("e5555555-0005-0000-0000-000000000001"), "Mr", "Mr", true, "mr", "SALUTATION" },
                    { new Guid("e5555555-0005-0000-0000-000000000002"), "Mrs", "Mrs", true, "mrs", "SALUTATION" },
                    { new Guid("e5555555-0005-0000-0000-000000000003"), "Ms", "Ms", true, "ms", "SALUTATION" },
                    { new Guid("e5555555-0005-0000-0000-000000000004"), "Doctor", "Dr", true, "dr", "SALUTATION" },
                    { new Guid("e5555555-0005-0000-0000-000000000005"), "Master", "Master", true, "master", "SALUTATION" },
                    { new Guid("e5555555-0006-0000-0000-000000000001"), "Spouse", "Spouse", true, "spouse", "RELATIONSHIP_TYPE" },
                    { new Guid("e5555555-0006-0000-0000-000000000002"), "Parent", "Parent", true, "parent", "RELATIONSHIP_TYPE" },
                    { new Guid("e5555555-0006-0000-0000-000000000003"), "Sibling", "Sibling", true, "sibling", "RELATIONSHIP_TYPE" },
                    { new Guid("e5555555-0006-0000-0000-000000000004"), "Child", "Child", true, "child", "RELATIONSHIP_TYPE" },
                    { new Guid("e5555555-0006-0000-0000-000000000005"), "Friend", "Friend", true, "friend", "RELATIONSHIP_TYPE" },
                    { new Guid("e5555555-0006-0000-0000-000000000006"), "Other", "Other", true, "other", "RELATIONSHIP_TYPE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0002-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0002-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0003-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0003-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0003-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0004-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0005-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0005-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0005-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0005-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0005-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0006-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0006-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0006-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0006-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0006-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0006-0000-0000-000000000006"));

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000001"),
                columns: new[] { "key", "type" },
                values: new object[] { "Doctor", "EmployeeType" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000002"),
                columns: new[] { "key", "type" },
                values: new object[] { "Nurse", "EmployeeType" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000003"),
                columns: new[] { "key", "type" },
                values: new object[] { "Receptionist", "EmployeeType" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000004"),
                columns: new[] { "key", "type" },
                values: new object[] { "Admin", "EmployeeType" });

            migrationBuilder.UpdateData(
                table: "master_values",
                keyColumn: "id",
                keyValue: new Guid("e5555555-0000-0000-0000-000000000005"),
                columns: new[] { "key", "type" },
                values: new object[] { "Other", "EmployeeType" });
        }
    }
}
