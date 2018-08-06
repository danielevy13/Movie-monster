using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMonste.Migrations
{
    public partial class AddMovieSaleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Sale_SaleID",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_SaleID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "Movie");

            migrationBuilder.CreateTable(
                name: "MovieSale",
                columns: table => new
                {
                    SaleID = table.Column<string>(nullable: false),
                    MovieID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSale", x => new { x.MovieID, x.SaleID });
                    table.ForeignKey(
                        name: "FK_MovieSale_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSale_Sale_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sale",
                        principalColumn: "SaleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieSale_SaleID",
                table: "MovieSale",
                column: "SaleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieSale");

            migrationBuilder.AddColumn<string>(
                name: "SaleID",
                table: "Movie",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_SaleID",
                table: "Movie",
                column: "SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Sale_SaleID",
                table: "Movie",
                column: "SaleID",
                principalTable: "Sale",
                principalColumn: "SaleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
