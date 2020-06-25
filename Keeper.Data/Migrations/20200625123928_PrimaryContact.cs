using Microsoft.EntityFrameworkCore.Migrations;

namespace Keeper.Data.Migrations
{
    public partial class PrimaryContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Primary",
                table: "Contacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Primary",
                table: "Contacts");
        }
    }
}
