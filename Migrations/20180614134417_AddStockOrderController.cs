using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class AddStockOrderController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockOrderID",
                table: "Movie",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockOrder",
                columns: table => new
                {
                    StockOrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierID = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrder", x => x.StockOrderID);
                    table.ForeignKey(
                        name: "FK_StockOrder_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_StockOrderID",
                table: "Movie",
                column: "StockOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrder_SupplierID",
                table: "StockOrder",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_StockOrder_StockOrderID",
                table: "Movie",
                column: "StockOrderID",
                principalTable: "StockOrder",
                principalColumn: "StockOrderID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_StockOrder_StockOrderID",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "StockOrder");

            migrationBuilder.DropIndex(
                name: "IX_Movie_StockOrderID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "StockOrderID",
                table: "Movie");
        }
    }
}
