using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class AddColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Admin.Users",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Admin.Users",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 11, 36, 56, 428, DateTimeKind.Local).AddTicks(5397), new DateTime(2022, 8, 31, 11, 36, 56, 428, DateTimeKind.Local).AddTicks(5411) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 11, 36, 56, 428, DateTimeKind.Local).AddTicks(5415), new DateTime(2022, 8, 31, 11, 36, 56, 428, DateTimeKind.Local).AddTicks(5416) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 11, 36, 56, 428, DateTimeKind.Local).AddTicks(5416), new DateTime(2022, 8, 31, 11, 36, 56, 428, DateTimeKind.Local).AddTicks(5417) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Admin.Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Admin.Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 11, 35, 11, 856, DateTimeKind.Local).AddTicks(9347), new DateTime(2022, 8, 31, 11, 35, 11, 856, DateTimeKind.Local).AddTicks(9356) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 11, 35, 11, 856, DateTimeKind.Local).AddTicks(9357), new DateTime(2022, 8, 31, 11, 35, 11, 856, DateTimeKind.Local).AddTicks(9358) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 11, 35, 11, 856, DateTimeKind.Local).AddTicks(9359), new DateTime(2022, 8, 31, 11, 35, 11, 856, DateTimeKind.Local).AddTicks(9359) });
        }
    }
}
