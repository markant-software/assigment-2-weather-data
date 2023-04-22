using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Assigment2WeatherData.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Wind = table.Column<float>(type: "real", nullable: false),
                    Clouds = table.Column<int>(type: "int", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherData_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, "Macedonia", 42f, 21.43f, "Skopje" },
                    { 2, "Macedonia", 41.03f, 21.34f, "Bitola" },
                    { 3, "Denmark", 55.67f, 12.58f, "Copenhagen" },
                    { 4, "Denmark", 55.4f, 10.38f, "Odense" },
                    { 5, "Norway", 59.92f, 10.75f, "Oslo" },
                    { 6, "Norway", 60.39f, 5.32f, "Bergen" },
                    { 7, "United Kingdom", 51.52f, -0.11f, "London" },
                    { 8, "United Kingdom", 53.48f, -2.25f, "Manchester" },
                    { 9, "Spain", 40.4f, -3.68f, "Madrid" },
                    { 10, "Spain", 10.18f, -68f, "Valencia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_CityId",
                table: "WeatherData",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
