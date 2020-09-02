using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_ArabicName",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_EnglishName",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Coupons");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Coupons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                table: "Coupons",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_Code",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Coupons");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "Coupons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Coupons",
                type: "nvarchar(450)",
                nullable: true);

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
        }
    }
}
