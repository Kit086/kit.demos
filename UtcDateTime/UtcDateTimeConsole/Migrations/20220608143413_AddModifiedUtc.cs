using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtcDateTimeConsole.Migrations
{
    public partial class AddModifiedUtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedUtc",
                table: "Products",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedUtc",
                table: "Products");
        }
    }
}
