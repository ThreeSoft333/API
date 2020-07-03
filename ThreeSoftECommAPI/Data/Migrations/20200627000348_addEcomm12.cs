using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_product_ArabicName",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_EnglishName",
                table: "product");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "product",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "product",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "product",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                table: "product",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "colorId",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sizeId",
                table: "product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_colorId",
                table: "product",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_product_sizeId",
                table: "product",
                column: "sizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_ProductColors_colorId",
                table: "product",
                column: "colorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_ProductSizes_sizeId",
                table: "product",
                column: "sizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_ProductColors_colorId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_ProductSizes_sizeId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_colorId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_sizeId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "colorId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "sizeId",
                table: "product");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedAt",
                table: "product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "product",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                table: "product",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_ArabicName",
                table: "product",
                column: "ArabicName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_EnglishName",
                table: "product",
                column: "EnglishName",
                unique: true);
        }
    }
}
