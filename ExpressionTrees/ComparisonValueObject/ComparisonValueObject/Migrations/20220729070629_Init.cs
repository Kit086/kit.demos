using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparisonValueObject.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Address_Country = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Address_Province = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Address_City = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Address_Detail = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
