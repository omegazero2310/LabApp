using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLab.Migrations
{
    public partial class addSeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9630), new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9641) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9643), new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9644) });

            migrationBuilder.UpdateData(
                table: "Admin.Parts",
                keyColumn: "PartID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9645), new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9645) });

            migrationBuilder.InsertData(
                table: "Admin.Users",
                columns: new[] { "UserName", "AccountStatus", "DateCreated", "DateModified", "DisplayName", "HashedPassword", "Id", "IsActive", "IsResetPassword", "PhoneNumber", "ProfilePictureName", "Salt", "UserCreated", "UserModified" },
                values: new object[] { "Admin", "Normal", new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9750), new DateTime(2022, 9, 5, 10, 32, 29, 529, DateTimeKind.Local).AddTicks(9751), "Administrator", "j3oEIiMWevilRp5y5/mzfETFHoGnBgrj23oQuiTNtRM=", new Guid("fdb4a328-d81a-428c-8be7-214248a39c94"), true, false, "09781231234", "", "kPg2GW/IblRkqFtMPrhmbw==", "SeedSystem", "SeedSystem" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin.Users",
                keyColumn: "UserName",
                keyValue: "Admin");

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
    }
}
