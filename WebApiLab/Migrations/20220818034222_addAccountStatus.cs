using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class addAccountStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountStatus",
                table: "Admin.Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "Admin.Users");
        }
    }
}
