using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog_Alpha.Data.Migrations
{
    public partial class CategoryRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified_At",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Modified_By",
                table: "Categories",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Modified_At",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Modified_By",
                table: "Categories");
        }
    }
}
