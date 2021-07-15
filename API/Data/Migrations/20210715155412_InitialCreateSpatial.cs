using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class InitialCreateSpatial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "zip",
                table: "Address",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "streetAddress",
                table: "Address",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Address",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Address",
                newName: "City");

            migrationBuilder.AddPrimaryKey(
                name: "PK_adress",
                table: "Address",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "spatial",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Area = table.Column<string>(type: "TEXT", nullable: true),
                    center_Lat = table.Column<string>(type: "TEXT", nullable: true),
                    center_Long = table.Column<string>(type: "TEXT", nullable: true),
                    dateaccessed = table.Column<DateTime>(type: "TEXT", nullable: false),
                    imagebyte = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spatial", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spatial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_adress",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Address",
                newName: "zip");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Address",
                newName: "streetAddress");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Address",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Address",
                newName: "city");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "ID");
        }
    }
}
