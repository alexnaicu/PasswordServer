using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordServer.Api.Migrations
{
    public partial class RenamedColumnsWithUtcSuffix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidUntil",
                table: "Passwords",
                newName: "ValidUntilUtc");

            migrationBuilder.RenameColumn(
                name: "ValidFrom",
                table: "Passwords",
                newName: "ValidFromUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidUntilUtc",
                table: "Passwords",
                newName: "ValidUntil");

            migrationBuilder.RenameColumn(
                name: "ValidFromUtc",
                table: "Passwords",
                newName: "ValidFrom");
        }
    }
}
