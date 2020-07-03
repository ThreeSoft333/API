using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductAttrId",
                table: "cartItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_ProductAttrId",
                table: "cartItems",
                column: "ProductAttrId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_ProductAttributes_ProductAttrId",
                table: "cartItems",
                column: "ProductAttrId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_ProductAttributes_ProductAttrId",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_ProductAttrId",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "ProductAttrId",
                table: "cartItems");
        }
    }
}
