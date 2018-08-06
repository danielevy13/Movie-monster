using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMonste.Migrations
{
    public partial class AddMovieStockOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieStockOrder",
                columns: table => new
                {
                    StockOrderID = table.Column<string>(nullable: false),
                    MovieID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStockOrder", x => new { x.MovieID, x.StockOrderID });
                    table.ForeignKey(
                        name: "FK_MovieStockOrder_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieStockOrder_StockOrder_StockOrderID",
                        column: x => x.StockOrderID,
                        principalTable: "StockOrder",
                        principalColumn: "StockOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieStockOrder_StockOrderID",
                table: "MovieStockOrder",
                column: "StockOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieStockOrder");
        }
    }
}
