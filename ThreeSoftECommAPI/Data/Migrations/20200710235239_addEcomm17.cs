using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "DECIMAL (25,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "OrderItems",
                type: "DECIMAL (25,3)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderItems");
        }
    }
}
