using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_3.Migrations
{
    public partial class addedEmailAndTele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Owners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Owners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Owners");
        }
    }
}
