using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_ProductAttributes_ProductAttrId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_ProductColors_ProductColorId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_ProductSizes_ProductSizeId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductAttributes_ProductAttrId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductColors_ProductColorId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductSizes_ProductSizeId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductAttrId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductColorId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductSizeId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_ProductAttrId",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_ProductColorId",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_ProductSizeId",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "ProductAttrId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductAttrId",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "cartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductAttrId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductColorId",
                table: "OrderItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductSizeId",
                table: "OrderItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductAttrId",
                table: "cartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductColorId",
                table: "cartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductSizeId",
                table: "cartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductAttrId",
                table: "OrderItems",
                column: "ProductAttrId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductColorId",
                table: "OrderItems",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductSizeId",
                table: "OrderItems",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_ProductAttrId",
                table: "cartItems",
                column: "ProductAttrId");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_ProductColorId",
                table: "cartItems",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_ProductSizeId",
                table: "cartItems",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_ProductAttributes_ProductAttrId",
                table: "cartItems",
                column: "ProductAttrId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_ProductColors_ProductColorId",
                table: "cartItems",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_ProductSizes_ProductSizeId",
                table: "cartItems",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductAttributes_ProductAttrId",
                table: "OrderItems",
                column: "ProductAttrId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductColors_ProductColorId",
                table: "OrderItems",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductSizes_ProductSizeId",
                table: "OrderItems",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
