using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class addIndexEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "UserNotification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 16, 20, 10, 804, DateTimeKind.Local).AddTicks(5722),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(7800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "UserLogin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 16, 20, 10, 804, DateTimeKind.Local).AddTicks(5140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(7185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 16, 20, 10, 804, DateTimeKind.Local).AddTicks(3552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(5641));

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Email",
                table: "UserInfo",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfo_Email",
                table: "UserInfo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "UserNotification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(7800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 16, 20, 10, 804, DateTimeKind.Local).AddTicks(5722));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "UserLogin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(7185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 16, 20, 10, 804, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(5641),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 17, 16, 20, 10, 804, DateTimeKind.Local).AddTicks(3552));
        }
    }
}
