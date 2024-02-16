using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportManagement.Migrations
{
    /// <inheritdoc />
    public partial class addairportcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "airportCode",
                table: "tblAirport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "airportCode",
                table: "tblAirport");
        }
    }
}
