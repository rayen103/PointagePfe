using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeEmployeCodeBusLatLngToEmployeAndLatLngToBus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeBus",
                table: "Employe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Employe",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Employe",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeEmploye",
                table: "Employe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "EmployeSimple");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Bus",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Bus",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeBus",
                table: "Employe");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Employe");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Employe");

            migrationBuilder.DropColumn(
                name: "TypeEmploye",
                table: "Employe");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Bus");
        }
    }
}
