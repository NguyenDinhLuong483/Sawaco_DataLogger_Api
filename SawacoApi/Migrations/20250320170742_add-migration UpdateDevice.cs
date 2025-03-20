using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawacoApi.API.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationUpdateDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AlarmTime",
                table: "GPSDevices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Emergency",
                table: "GPSDevices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmTime",
                table: "GPSDevices");

            migrationBuilder.DropColumn(
                name: "Emergency",
                table: "GPSDevices");
        }
    }
}
