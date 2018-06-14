using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class AddCustomerIDtoSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerAddress_ApartmentNumber",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_City",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_State",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress_StreetName",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerAddress_ZipCode",
                table: "Customer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddress_ApartmentNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_City",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_State",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_StreetName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerAddress_ZipCode",
                table: "Customer");
        }
    }
}
