using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "offerPrice",
                table: "Offers",
                type: "DECIMAL (15,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (5,3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "offerPrice",
                table: "Offers",
                type: "DECIMAL (5,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (15,3)");
        }
    }
}
