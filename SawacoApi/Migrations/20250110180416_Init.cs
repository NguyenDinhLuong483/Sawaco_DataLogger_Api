using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SawacoApi.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatteryHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "GPSDevices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerPhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Battery = table.Column<double>(type: "float", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Stolen = table.Column<bool>(type: "bit", nullable: false),
                    Bluetooth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SMSNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Package = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPSDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPSDevices_Customers_CustomerPhoneNumber",
                        column: x => x.CustomerPhoneNumber,
                        principalTable: "Customers",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GPSObjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerPhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GPSDeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SafeRadius = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Connected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPSObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPSObjects_Customers_CustomerPhoneNumber",
                        column: x => x.CustomerPhoneNumber,
                        principalTable: "Customers",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevicePositionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPSDeviceId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePositionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevicePositionHistories_GPSDevices_GPSDeviceId",
                        column: x => x.GPSDeviceId,
                        principalTable: "GPSDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StolenLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPSDeviceId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Battery = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StolenLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StolenLines_GPSDevices_GPSDeviceId",
                        column: x => x.GPSDeviceId,
                        principalTable: "GPSDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObjectPositionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPSObjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectPositionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectPositionHistories_GPSObjects_GPSObjectId",
                        column: x => x.GPSObjectId,
                        principalTable: "GPSObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicePositionHistories_GPSDeviceId",
                table: "DevicePositionHistories",
                column: "GPSDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_GPSDevices_CustomerPhoneNumber",
                table: "GPSDevices",
                column: "CustomerPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_GPSObjects_CustomerPhoneNumber",
                table: "GPSObjects",
                column: "CustomerPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectPositionHistories_GPSObjectId",
                table: "ObjectPositionHistories",
                column: "GPSObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StolenLines_GPSDeviceId",
                table: "StolenLines",
                column: "GPSDeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryHistories");

            migrationBuilder.DropTable(
                name: "DevicePositionHistories");

            migrationBuilder.DropTable(
                name: "ObjectPositionHistories");

            migrationBuilder.DropTable(
                name: "StolenLines");

            migrationBuilder.DropTable(
                name: "GPSObjects");

            migrationBuilder.DropTable(
                name: "GPSDevices");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
