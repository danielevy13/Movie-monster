using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class AddPriceToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "UnitPrice_Retail",
                table: "Movie",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice_Wholesale",
                table: "Movie",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Movie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice_Retail",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "UnitPrice_Wholesale",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Movie");
        }
    }
}
