using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMonste.Migrations
{
    public partial class AddQuantityMovieSaleAndStockOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_StockOrder_StockOrderID",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_StockOrderID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "StockOrderID",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MovieStockOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MovieSale",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MovieStockOrder");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MovieSale");

            migrationBuilder.AddColumn<string>(
                name: "StockOrderID",
                table: "Movie",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_StockOrderID",
                table: "Movie",
                column: "StockOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_StockOrder_StockOrderID",
                table: "Movie",
                column: "StockOrderID",
                principalTable: "StockOrder",
                principalColumn: "StockOrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
