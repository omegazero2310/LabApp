using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class AddUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Admin.Staffs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Admin.Staffs");
        }
    }
}
