using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusRuntimeOperationsCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentOccupancy",
                table: "Bus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOccupancyUpdateAt",
                table: "Bus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPositionAt",
                table: "Bus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusRuntimeEvent",
                columns: table => new
                {
                    BusRuntimeEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IMEI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Occupancy = table.Column<int>(type: "int", nullable: true),
                    OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRuntimeEvent", x => x.BusRuntimeEventId);
                    table.ForeignKey(
                        name: "FK_BusRuntimeEvent_Bus_BusId",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusRuntimeEvent_BusId",
                table: "BusRuntimeEvent",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRuntimeEvent_OccurredAtUtc",
                table: "BusRuntimeEvent",
                column: "OccurredAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusRuntimeEvent");

            migrationBuilder.DropColumn(
                name: "CurrentOccupancy",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "LastOccupancyUpdateAt",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "LastPositionAt",
                table: "Bus");
        }
    }
}
