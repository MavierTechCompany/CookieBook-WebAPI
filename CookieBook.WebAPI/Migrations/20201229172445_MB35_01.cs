using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookieBook.WebAPI.Migrations
{
    public partial class MB35_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Login", "PasswordHash", "RestoreKey", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 12, 29, 17, 24, 44, 996, DateTimeKind.Utc), 4116719210845653519m, new byte[] { 239, 230, 211, 6, 157, 112, 17, 26, 43, 201, 139, 219, 136, 21, 54, 113, 157, 34, 215, 228, 250, 35, 152, 245, 103, 172, 179, 73, 149, 50, 250, 61, 185, 186, 252, 9, 254, 199, 162, 22, 156, 174, 93, 63, 22, 209, 219, 46, 195, 180, 81, 179, 5, 95, 20, 229, 76, 39, 61, 150, 61, 112, 172, 255 }, "?DjC14!5G4vA", new byte[] { 250, 127, 13, 50, 20, 151, 124, 65, 139, 37, 176, 20, 132, 169, 244, 110, 235, 94, 207, 250, 254, 38, 54, 255, 193, 197, 204, 52, 88, 63, 57, 218, 245, 220, 231, 218, 147, 104, 138, 243, 191, 236, 204, 53, 140, 135, 34, 202, 208, 17, 122, 174, 102, 140, 181, 88, 116, 222, 237, 5, 90, 45, 128, 184, 103, 121, 7, 216, 196, 227, 61, 119, 198, 92, 164, 251, 182, 23, 72, 49, 186, 115, 171, 235, 182, 174, 152, 66, 18, 244, 234, 183, 145, 135, 223, 74, 254, 201, 226, 199, 52, 194, 52, 8, 131, 89, 13, 49, 172, 207, 227, 236, 106, 51, 223, 105, 140, 36, 78, 183, 220, 97, 118, 220, 44, 109, 202, 231 }, new DateTime(2020, 12, 29, 17, 24, 44, 996, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Rates");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Login", "PasswordHash", "RestoreKey", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 10, 11, 14, 57, 34, 246, DateTimeKind.Utc), 0m, new byte[] { 183, 105, 10, 89, 62, 12, 27, 51, 23, 220, 115, 172, 207, 52, 117, 56, 156, 239, 71, 65, 202, 228, 141, 227, 191, 109, 34, 96, 179, 137, 226, 168, 102, 136, 181, 155, 120, 183, 152, 3, 172, 219, 165, 199, 164, 220, 38, 193, 54, 102, 37, 42, 246, 18, 143, 160, 57, 201, 191, 130, 203, 132, 139, 116 }, "4RgL!i2v?!fH", new byte[] { 180, 216, 25, 69, 113, 7, 129, 7, 246, 33, 36, 7, 14, 214, 222, 225, 76, 171, 179, 30, 7, 57, 38, 130, 206, 44, 89, 236, 240, 157, 16, 105, 123, 139, 79, 252, 163, 46, 105, 205, 0, 115, 29, 188, 179, 193, 223, 20, 89, 42, 87, 151, 15, 229, 42, 220, 200, 11, 47, 170, 83, 224, 169, 66, 66, 89, 177, 48, 169, 68, 43, 167, 162, 36, 20, 230, 152, 175, 21, 7, 59, 35, 244, 226, 132, 211, 85, 49, 178, 170, 235, 68, 125, 154, 154, 191, 209, 110, 81, 154, 236, 187, 218, 200, 94, 124, 57, 37, 178, 231, 194, 82, 12, 207, 161, 185, 185, 153, 34, 255, 245, 217, 10, 206, 159, 181, 178, 179 }, new DateTime(2020, 10, 11, 14, 57, 34, 246, DateTimeKind.Utc) });
        }
    }
}
