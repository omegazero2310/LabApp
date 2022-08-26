using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class removeOldFieldAdminStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionID",
                table: "Admin.Staffs");

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 26, 15, 47, 39, 762, DateTimeKind.Local).AddTicks(8944), new DateTime(2022, 8, 26, 15, 47, 39, 762, DateTimeKind.Local).AddTicks(8958) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 26, 15, 47, 39, 762, DateTimeKind.Local).AddTicks(8960), new DateTime(2022, 8, 26, 15, 47, 39, 762, DateTimeKind.Local).AddTicks(8961) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 26, 15, 47, 39, 762, DateTimeKind.Local).AddTicks(8962), new DateTime(2022, 8, 26, 15, 47, 39, 762, DateTimeKind.Local).AddTicks(8962) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionID",
                table: "Admin.Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7328), new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7339) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7340), new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7341) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7342), new DateTime(2022, 8, 26, 14, 29, 3, 503, DateTimeKind.Local).AddTicks(7342) });
        }
    }
}
