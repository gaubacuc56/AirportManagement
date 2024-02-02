using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportManagement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAircraft",
                columns: table => new
                {
                    aircraftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    aircraftName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    aircraftCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAircraft", x => x.aircraftId);
                });

            migrationBuilder.CreateTable(
                name: "tblCountry",
                columns: table => new
                {
                    countryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    countryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountry", x => x.countryId);
                });

            migrationBuilder.CreateTable(
                name: "tblSystemRole",
                columns: table => new
                {
                    Role = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSystemRole", x => x.Role);
                });

            migrationBuilder.CreateTable(
                name: "tblCity",
                columns: table => new
                {
                    cityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    cityName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    countryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCity", x => x.cityId);
                    table.ForeignKey(
                        name: "FK_tblCity_tblCountry_countryId",
                        column: x => x.countryId,
                        principalTable: "tblCountry",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblAirport",
                columns: table => new
                {
                    airportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    airportName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAirport", x => x.airportId);
                    table.ForeignKey(
                        name: "FK_tblAirport_tblCity_cityId",
                        column: x => x.cityId,
                        principalTable: "tblCity",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblEmployee",
                columns: table => new
                {
                    employeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    employeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    employeePhone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    employeeAccount = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    employeePassword = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    empRole = table.Column<int>(type: "int", nullable: false),
                    airportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployee", x => x.employeeId);
                    table.CheckConstraint("ck_emp_role", "empRole IN (0, 1)");
                    table.ForeignKey(
                        name: "FK_tblEmployee_tblAirport_airportId",
                        column: x => x.airportId,
                        principalTable: "tblAirport",
                        principalColumn: "airportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRunway",
                columns: table => new
                {
                    runwayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    runwayCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    runwayLength = table.Column<int>(type: "int", nullable: false),
                    isRunning = table.Column<bool>(type: "bit", nullable: false),
                    airportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRunway", x => x.runwayId);
                    table.ForeignKey(
                        name: "FK_tblRunway_tblAirport_airportId",
                        column: x => x.airportId,
                        principalTable: "tblAirport",
                        principalColumn: "airportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblFlight",
                columns: table => new
                {
                    flightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flightCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    takeoffTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reachingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    aircraftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    runwayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFlight", x => x.flightId);
                    table.ForeignKey(
                        name: "FK_tblFlight_tblAircraft_aircraftId",
                        column: x => x.aircraftId,
                        principalTable: "tblAircraft",
                        principalColumn: "aircraftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFlight_tblCity_cityId",
                        column: x => x.cityId,
                        principalTable: "tblCity",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFlight_tblRunway_runwayId",
                        column: x => x.runwayId,
                        principalTable: "tblRunway",
                        principalColumn: "runwayId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPassenger",
                columns: table => new
                {
                    passengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    passengerDOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    passengerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passengerEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    passengerPhone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPassenger", x => x.passengerId);
                    table.ForeignKey(
                        name: "FK_tblPassenger_tblFlight_flightId",
                        column: x => x.flightId,
                        principalTable: "tblFlight",
                        principalColumn: "flightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblLuggage",
                columns: table => new
                {
                    luggageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    luggageWeight = table.Column<int>(type: "int", nullable: false),
                    passengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLuggage", x => x.luggageId);
                    table.ForeignKey(
                        name: "FK_tblLuggage_tblFlight_flightId",
                        column: x => x.flightId,
                        principalTable: "tblFlight",
                        principalColumn: "flightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblLuggage_tblPassenger_passengerId",
                        column: x => x.passengerId,
                        principalTable: "tblPassenger",
                        principalColumn: "passengerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAircraft_aircraftName",
                table: "tblAircraft",
                column: "aircraftName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblAirport_airportName",
                table: "tblAirport",
                column: "airportName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblAirport_cityId",
                table: "tblAirport",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCity_cityName",
                table: "tblCity",
                column: "cityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCity_countryId",
                table: "tblCity",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCountry_countryName",
                table: "tblCountry",
                column: "countryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployee_airportId",
                table: "tblEmployee",
                column: "airportId");

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployee_employeeAccount",
                table: "tblEmployee",
                column: "employeeAccount",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployee_employeeEmail",
                table: "tblEmployee",
                column: "employeeEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployee_employeePhone",
                table: "tblEmployee",
                column: "employeePhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblFlight_aircraftId",
                table: "tblFlight",
                column: "aircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFlight_cityId",
                table: "tblFlight",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFlight_flightCode",
                table: "tblFlight",
                column: "flightCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblFlight_runwayId",
                table: "tblFlight",
                column: "runwayId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLuggage_flightId",
                table: "tblLuggage",
                column: "flightId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLuggage_passengerId",
                table: "tblLuggage",
                column: "passengerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPassenger_flightId",
                table: "tblPassenger",
                column: "flightId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPassenger_passengerEmail",
                table: "tblPassenger",
                column: "passengerEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblPassenger_passengerName",
                table: "tblPassenger",
                column: "passengerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblPassenger_passengerPhone",
                table: "tblPassenger",
                column: "passengerPhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRunway_airportId",
                table: "tblRunway",
                column: "airportId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSystemRole_RoleName",
                table: "tblSystemRole",
                column: "RoleName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblEmployee");

            migrationBuilder.DropTable(
                name: "tblLuggage");

            migrationBuilder.DropTable(
                name: "tblSystemRole");

            migrationBuilder.DropTable(
                name: "tblPassenger");

            migrationBuilder.DropTable(
                name: "tblFlight");

            migrationBuilder.DropTable(
                name: "tblAircraft");

            migrationBuilder.DropTable(
                name: "tblRunway");

            migrationBuilder.DropTable(
                name: "tblAirport");

            migrationBuilder.DropTable(
                name: "tblCity");

            migrationBuilder.DropTable(
                name: "tblCountry");
        }
    }
}
