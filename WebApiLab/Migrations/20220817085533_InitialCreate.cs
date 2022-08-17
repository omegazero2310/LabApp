using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(5641)),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsNew = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TotalTrips = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    Salt = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    IsResetPassword = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(7185))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 8, 17, 15, 55, 33, 481, DateTimeKind.Local).AddTicks(7800)),
                    NotificationType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValue: "System"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false, defaultValue: "System"),
                    IsReaded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => x.NotificationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserNotification");
        }
    }
}
