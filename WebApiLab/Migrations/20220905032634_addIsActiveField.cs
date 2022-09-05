using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class addIsActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Admin.Users",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Admin.Staffs",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "UserModified",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreated",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "Admin.Parts",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Admin.Parts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Admin.Parts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "PartID",
                table: "Admin.Parts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Admin.Parts",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 9, 5, 10, 26, 34, 601, DateTimeKind.Local).AddTicks(8511), new DateTime(2022, 9, 5, 10, 26, 34, 601, DateTimeKind.Local).AddTicks(8520) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 9, 5, 10, 26, 34, 601, DateTimeKind.Local).AddTicks(8528), new DateTime(2022, 9, 5, 10, 26, 34, 601, DateTimeKind.Local).AddTicks(8528) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 9, 5, 10, 26, 34, 601, DateTimeKind.Local).AddTicks(8529), new DateTime(2022, 9, 5, 10, 26, 34, 601, DateTimeKind.Local).AddTicks(8529) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admin.Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admin.Parts");

            migrationBuilder.AlterColumn<string>(
                name: "UserModified",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreated",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Admin.Parts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Admin.Parts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "PartID",
                table: "Admin.Parts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", 0)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4122), new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4132) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4134), new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4134) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4135), new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4135) });
        }
    }
}
