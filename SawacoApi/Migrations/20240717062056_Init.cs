using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawacoApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loggers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Battery = table.Column<int>(type: "int", nullable: false),
                    Stolen = table.Column<bool>(type: "bit", nullable: false),
                    Bluetooth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loggers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StolenLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoggerId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StolenLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StolenLines_Loggers_LoggerId",
                        column: x => x.LoggerId,
                        principalTable: "Loggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StolenLines_LoggerId",
                table: "StolenLines",
                column: "LoggerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StolenLines");

            migrationBuilder.DropTable(
                name: "Loggers");
        }
    }
}
