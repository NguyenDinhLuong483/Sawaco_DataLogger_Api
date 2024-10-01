using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawacoApi.Migrations
{
    /// <inheritdoc />
    public partial class InitTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "Loggers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Loggers");
        }
    }
}
