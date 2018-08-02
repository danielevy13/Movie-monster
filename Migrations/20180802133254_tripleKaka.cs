using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMansterWebApp.Migrations
{
    public partial class tripleKaka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Customer_CustomerID1",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_CustomerID1",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "CustomerID1",
                table: "Sale");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Sale",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "SaleID",
                table: "Sale",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "SaleID",
                table: "Movie",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerID",
                table: "Sale",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Customer_CustomerID",
                table: "Sale",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Customer_CustomerID",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_CustomerID",
                table: "Sale");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Sale",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                table: "Sale",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "CustomerID1",
                table: "Sale",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                table: "Movie",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerID1",
                table: "Sale",
                column: "CustomerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Customer_CustomerID1",
                table: "Sale",
                column: "CustomerID1",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
