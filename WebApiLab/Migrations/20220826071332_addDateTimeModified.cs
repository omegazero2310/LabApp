using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class addDateTimeModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Admin.Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Admin.Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Admin.Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Admin.Staffs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Admin.Staffs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PartID",
                table: "Admin.Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Admin.Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Admin.Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Admin.Parts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Admin.Parts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Admin.Parts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Admin.Staffs_PartID",
                table: "Admin.Staffs",
                column: "PartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin.Staffs_Admin.Parts_PartID",
                table: "Admin.Staffs",
                column: "PartID",
                principalTable: "Admin.Parts",
                principalColumn: "PartID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin.Staffs_Admin.Parts_PartID",
                table: "Admin.Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Admin.Staffs_PartID",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Admin.Users");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Admin.Users");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Admin.Users");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "PartID",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Admin.Parts");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Admin.Parts");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Admin.Parts");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Admin.Parts");
        }
    }
}
