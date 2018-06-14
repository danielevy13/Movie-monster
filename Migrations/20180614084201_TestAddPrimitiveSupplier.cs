using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class TestAddPrimitiveSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Supplier",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Supplier");
        }
    }
}
