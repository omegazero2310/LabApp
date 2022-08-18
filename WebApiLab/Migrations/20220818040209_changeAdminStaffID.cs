using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class changeAdminStaffID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin.Staffs",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Admin.Staffs");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Admin.Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin.Staffs",
                table: "Admin.Staffs",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin.Staffs",
                table: "Admin.Staffs");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Admin.Staffs");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Admin.Staffs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin.Staffs",
                table: "Admin.Staffs",
                column: "UserID");
        }
    }
}
