using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductSizeId",
                table: "cartItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_ProductSizeId",
                table: "cartItems",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_ProductSizes_ProductSizeId",
                table: "cartItems",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_ProductSizes_ProductSizeId",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_ProductSizeId",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "cartItems");
        }
    }
}
