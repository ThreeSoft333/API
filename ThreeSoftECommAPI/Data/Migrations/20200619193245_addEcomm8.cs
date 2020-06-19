﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeSoftECommAPI.Data.Migrations
{
    public partial class addEcomm8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "product",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "product",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "Orders",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "offerPrice",
                table: "Offers",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Coupons",
                type: "DECIMAL (25,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (20,3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "product",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "product",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "Orders",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "offerPrice",
                table: "Offers",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Coupons",
                type: "DECIMAL (20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL (25,3)");
        }
    }
}
