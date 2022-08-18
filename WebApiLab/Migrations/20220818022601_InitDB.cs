using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin.Staffs",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin.Staffs", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Admin.Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    IsResetPassword = table.Column<bool>(type: "bit", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin.Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin.Staffs");

            migrationBuilder.DropTable(
                name: "Admin.Users");
        }
    }
}
