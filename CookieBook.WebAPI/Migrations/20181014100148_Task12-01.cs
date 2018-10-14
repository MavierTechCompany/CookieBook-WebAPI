using Microsoft.EntityFrameworkCore.Migrations;

namespace CookieBook.WebAPI.Migrations
{
    public partial class Task1201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Images_UserImageId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserImageId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "UserRef",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserRef",
                table: "Images",
                column: "UserRef",
                unique: true,
                filter: "[UserRef] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accounts_UserRef",
                table: "Images",
                column: "UserRef",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accounts_UserRef",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UserRef",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "UserImageId",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserImageId",
                table: "Accounts",
                column: "UserImageId",
                unique: true,
                filter: "[UserImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Images_UserImageId",
                table: "Accounts",
                column: "UserImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
