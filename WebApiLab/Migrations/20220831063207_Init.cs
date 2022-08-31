using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin.Parts",
                columns: table => new
                {
                    PartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin.Parts", x => x.PartID);
                });

            migrationBuilder.CreateTable(
                name: "Admin.Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    IsResetPassword = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountStatus = table.Column<string>(type: "varchar(10)", nullable: false),
                    ProfilePictureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin.Users", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Admin.Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PartID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin.Staffs", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Admin.Staffs_Admin.Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Admin.Parts",
                        principalColumn: "PartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admin.Parts",
                columns: new[] { "PartID", "DateCreated", "DateModified", "PartName", "UserCreated", "UserModified" },
                values: new object[] { 1, new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4122), new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4132), "Nhân viên", "Seed", "Seed" });

            migrationBuilder.InsertData(
                table: "Admin.Parts",
                columns: new[] { "PartID", "DateCreated", "DateModified", "PartName", "UserCreated", "UserModified" },
                values: new object[] { 2, new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4134), new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4134), "Trưởng phòng", "Seed", "Seed" });

            migrationBuilder.InsertData(
                table: "Admin.Parts",
                columns: new[] { "PartID", "DateCreated", "DateModified", "PartName", "UserCreated", "UserModified" },
                values: new object[] { 3, new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4135), new DateTime(2022, 8, 31, 13, 32, 7, 522, DateTimeKind.Local).AddTicks(4135), "Giám đốc", "Seed", "Seed" });

            migrationBuilder.CreateIndex(
                name: "IX_Admin.Staffs_PartID",
                table: "Admin.Staffs",
                column: "PartID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin.Staffs");

            migrationBuilder.DropTable(
                name: "Admin.Users");

            migrationBuilder.DropTable(
                name: "Admin.Parts");
        }
    }
}
