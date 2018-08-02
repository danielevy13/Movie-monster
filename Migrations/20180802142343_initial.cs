using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMonste.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Gender = table.Column<string>(nullable: false),
                    CustomerAddress_State = table.Column<string>(nullable: true),
                    CustomerAddress_City = table.Column<string>(nullable: true),
                    CustomerAddress_StreetName = table.Column<string>(nullable: true),
                    CustomerAddress_ApartmentNumber = table.Column<int>(nullable: false),
                    CustomerAddress_ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleID = table.Column<string>(nullable: false),
                    CustomerID = table.Column<string>(nullable: true),
                    Purchased = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Genere = table.Column<string>(nullable: true),
                    UnitsInStock = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Actors = table.Column<string>(nullable: true),
                    MinAge = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    UnitPrice_Wholesale = table.Column<float>(nullable: false),
                    UnitPrice_Retail = table.Column<float>(nullable: false),
                    SaleID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieID);
                    table.ForeignKey(
                        name: "FK_Movie_Sale_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sale",
                        principalColumn: "SaleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_SaleID",
                table: "Movie",
                column: "SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerID",
                table: "Sale",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
