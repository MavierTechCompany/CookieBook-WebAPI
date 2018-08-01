using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookieBook.WebAPI.Migrations
{
    public partial class Task0201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "RestoreKey");

            migrationBuilder.AlterColumn<decimal>(
                name: "Login",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "UserEmail",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "RestoreKey",
                table: "Accounts",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
