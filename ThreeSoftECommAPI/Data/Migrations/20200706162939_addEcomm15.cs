using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "UserAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "UserAddresses");
        }
    }
}
