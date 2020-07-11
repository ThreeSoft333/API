using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "product",
                type: "DECIMAL (25,3)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "product",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)",
                oldDefaultValue: 0m);
        }
    }
}
