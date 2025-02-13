using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawacoApi.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBattery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GPSDeviceId",
                table: "BatteryHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GPSDeviceId",
                table: "BatteryHistories");
        }
    }
}
