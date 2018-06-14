using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class AddAdrressSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierAddress_ApartmentNumber",
                table: "Supplier",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SupplierAddress_City",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierAddress_State",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierAddress_StreetName",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierAddress_ZipCode",
                table: "Supplier",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierAddress_ApartmentNumber",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SupplierAddress_City",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SupplierAddress_State",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SupplierAddress_StreetName",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SupplierAddress_ZipCode",
                table: "Supplier");
        }
    }
}
