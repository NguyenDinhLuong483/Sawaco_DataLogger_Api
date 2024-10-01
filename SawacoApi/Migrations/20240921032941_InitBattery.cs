using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawacoApi.Migrations
{
    /// <inheritdoc />
    public partial class InitBattery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Battery",
                table: "StolenLines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Battery",
                table: "StolenLines");
        }
    }
}
