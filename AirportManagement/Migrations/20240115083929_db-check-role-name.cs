using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportManagement.Migrations
{
    /// <inheritdoc />
    public partial class dbcheckrolename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "ck_role_name",
                table: "tblSystemRole",
                sql: "RoleName IN ('Admin', 'Employee')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_role_name",
                table: "tblSystemRole");
        }
    }
}
