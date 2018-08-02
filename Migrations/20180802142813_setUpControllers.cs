using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMonste.Migrations
{
    public partial class setUpControllers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StockOrderID",
                table: "Movie",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true),
                    SupplierAddress_State = table.Column<string>(nullable: true),
                    SupplierAddress_City = table.Column<string>(nullable: true),
                    SupplierAddress_StreetName = table.Column<string>(nullable: true),
                    SupplierAddress_ApartmentNumber = table.Column<int>(nullable: false),
                    SupplierAddress_ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "StockOrder",
                columns: table => new
                {
                    StockOrderID = table.Column<string>(nullable: false),
                    SupplierID = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Movie_StockOrderID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "StockOrderID",
                table: "Movie");
        }
    }
}
