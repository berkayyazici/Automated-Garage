using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomatedGarage.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkSpotId = table.Column<int>(type: "int", nullable: false),
                    ParkSpotFloor = table.Column<int>(type: "int", nullable: false),
                    ticketNumber = table.Column<int>(type: "int", nullable: false),
                    ParkSpotName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkSpotDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
