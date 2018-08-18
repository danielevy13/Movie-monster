using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieMonster.Data.Migrations
{
    public partial class connectBetweenUserCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserID",
                table: "Customer",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_UserID",
                table: "Customer",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_UserID",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Customer");
        }
    }
}
