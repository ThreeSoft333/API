using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_category_Name",
                table: "category");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "subCategory");

            migrationBuilder.DropColumn(
                name: "Descreption",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "category");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Advertisings");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "subCategory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "subCategory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicDescreption",
                table: "ProductReviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishDescreption",
                table: "ProductReviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "ProductColors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "ProductColors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "ProductAttributes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "ProductAttributes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicDescription",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishDescription",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "Coupons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Coupons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "category",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "category",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "Brand",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Brand",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicDescription",
                table: "Advertisings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishDescription",
                table: "Advertisings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subCategory_ArabicName",
                table: "subCategory",
                column: "ArabicName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subCategory_EnglishName",
                table: "subCategory",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ArabicName",
                table: "ProductColors",
                column: "ArabicName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_EnglishName",
                table: "ProductColors",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ArabicName",
                table: "ProductAttributes",
                column: "ArabicName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_EnglishName",
                table: "ProductAttributes",
                column: "EnglishName",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ArabicName",
                table: "Coupons",
                column: "ArabicName",
                unique: true,
                filter: "[ArabicName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_EnglishName",
                table: "Coupons",
                column: "EnglishName",
                unique: true,
                filter: "[EnglishName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_category_ArabicName",
                table: "category",
                column: "ArabicName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_EnglishName",
                table: "category",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ArabicName",
                table: "Brand",
                column: "ArabicName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_EnglishName",
                table: "Brand",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_ArabicDescription",
                table: "Advertisings",
                column: "ArabicDescription",
                unique: true,
                filter: "[ArabicDescription] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_EnglishDescription",
                table: "Advertisings",
                column: "EnglishDescription",
                unique: true,
                filter: "[EnglishDescription] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_subCategory_ArabicName",
                table: "subCategory");

            migrationBuilder.DropIndex(
                name: "IX_subCategory_EnglishName",
                table: "subCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ArabicName",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_EnglishName",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ArabicName",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_EnglishName",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_product_ArabicName",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_EnglishName",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_ArabicName",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_EnglishName",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_category_ArabicName",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_EnglishName",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ArabicName",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_EnglishName",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Advertisings_ArabicDescription",
                table: "Advertisings");

            migrationBuilder.DropIndex(
                name: "IX_Advertisings_EnglishDescription",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "subCategory");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "subCategory");

            migrationBuilder.DropColumn(
                name: "ArabicDescreption",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "EnglishDescreption",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ArabicDescription",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "product");

            migrationBuilder.DropColumn(
                name: "EnglishDescription",
                table: "product");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "category");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "category");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ArabicDescription",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "EnglishDescription",
                table: "Advertisings");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "subCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descreption",
                table: "ProductReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "category",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Advertisings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_Name",
                table: "category",
                column: "Name",
                unique: true);
        }
    }
}
