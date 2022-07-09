using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoMapperDemoConsole.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    DetailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Age", "CreatedUtc", "FirstName", "LastName", "ModifiedUtc" },
                values: new object[] { 1, 63, new DateTime(2022, 6, 28, 11, 5, 24, 31, DateTimeKind.Utc).AddTicks(3025), "Bei", "Liu", new DateTime(2022, 7, 8, 11, 5, 24, 31, DateTimeKind.Utc).AddTicks(3026) });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Age", "CreatedUtc", "FirstName", "LastName", "ModifiedUtc" },
                values: new object[] { 2, 60, new DateTime(2022, 6, 28, 11, 5, 24, 31, DateTimeKind.Utc).AddTicks(3037), "Yu", "Guan", new DateTime(2022, 7, 8, 11, 5, 24, 31, DateTimeKind.Utc).AddTicks(3037) });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Age", "CreatedUtc", "FirstName", "LastName", "ModifiedUtc" },
                values: new object[] { 3, 58, new DateTime(2022, 6, 28, 11, 5, 24, 31, DateTimeKind.Utc).AddTicks(3039), "Fei", "Zhang", null });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "EmailAddress", "Password", "PersonId", "Username" },
                values: new object[] { 1, "liuxuande@han.com", "123456", 1, "liuxuande" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "EmailAddress", "Password", "PersonId", "Username" },
                values: new object[] { 2, null, "123456", 1, "liuyuzhou" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "EmailAddress", "Password", "PersonId", "Username" },
                values: new object[] { 3, "guanyunchang@han.com", "123456", 2, "guanyunchang" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "EmailAddress", "Password", "PersonId", "Username" },
                values: new object[] { 4, null, "123456", 2, "meirangong" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Country", "DetailAddress", "PersonId", "Province" },
                values: new object[] { 1, "zhuoxian", "han", "liubeijia", 1, "zhuojun" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Country", "DetailAddress", "PersonId", "Province" },
                values: new object[] { 2, "unknown", "han", "zhangfeijia", 3, "zhuojun" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonId",
                table: "Accounts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                column: "PersonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
