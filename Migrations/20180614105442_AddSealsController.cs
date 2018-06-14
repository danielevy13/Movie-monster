using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class AddSealsController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                table: "Movie",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(nullable: false),
                    Purchased = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    CustomerID1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerID1",
                        column: x => x.CustomerID1,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_SaleID",
                table: "Movie",
                column: "SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerID1",
                table: "Sale",
                column: "CustomerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Sale_SaleID",
                table: "Movie",
                column: "SaleID",
                principalTable: "Sale",
                principalColumn: "SaleID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Sale_SaleID",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Movie_SaleID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "Movie");
        }
    }
}
