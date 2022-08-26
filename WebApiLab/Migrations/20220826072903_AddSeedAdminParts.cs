using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class AddSeedAdminParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admin.Parts",
                columns: new[] { "PartID", "DateCreated", "DateModified", "PartName", "UserCreated", "UserModified" },
                values: new object[] { 1, new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7328), new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7339), "Nhân viên", "Seed", "Seed" });

            migrationBuilder.InsertData(
                table: "Admin.Parts",
                columns: new[] { "PartID", "DateCreated", "DateModified", "PartName", "UserCreated", "UserModified" },
                values: new object[] { 2, new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7340), new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7341), "Trưởng phòng", "Seed", "Seed" });

            migrationBuilder.InsertData(
                table: "Admin.Parts",
                columns: new[] { "PartID", "DateCreated", "DateModified", "PartName", "UserCreated", "UserModified" },
                values: new object[] { 3, new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7342), new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7342), "Giám đốc", "Seed", "Seed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3);
        }
    }
}
