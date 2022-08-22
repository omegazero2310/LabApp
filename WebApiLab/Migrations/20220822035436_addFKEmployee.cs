using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class addFKEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Admin.Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Admin.Staffs",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Admin.Staffs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Admin.Users_ID",
                table: "Admin.Users",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin.Users_Admin.Staffs_ID",
                table: "Admin.Users",
                column: "ID",
                principalTable: "Admin.Staffs",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin.Users_Admin.Staffs_ID",
                table: "Admin.Users");

            migrationBuilder.DropIndex(
                name: "IX_Admin.Users_ID",
                table: "Admin.Users");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Admin.Users");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Admin.Staffs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Admin.Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
