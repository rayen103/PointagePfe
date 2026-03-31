using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHighPriorityEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodePCArrivee",
                table: "Circuit",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePCDepart",
                table: "Circuit",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Couleur",
                table: "Circuit",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DistanceKm",
                table: "Circuit",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DureeMinutes",
                table: "Circuit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    BusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroIMM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModelBus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IMEI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Capacite = table.Column<int>(type: "int", nullable: true),
                    CodeCircuit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AppSagem = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_Bus_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CircuitPointCollecte",
                columns: table => new
                {
                    CircuitPointCollecteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodePointCollecte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibellePointCollecte = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    Ordre = table.Column<int>(type: "int", nullable: true),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircuitPointCollecte", x => x.CircuitPointCollecteId);
                    table.ForeignKey(
                        name: "FK_CircuitPointCollecte_Circuit_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "Circuit",
                        principalColumn: "CircuitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdreTravailDetail",
                columns: table => new
                {
                    OrdreTravailDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrdreTravailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeArticle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodeEntrepot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeUnite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LibelleArticle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PrixUnitaireHT = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    Quantite = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    TauxTVA = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    Montant = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdreTravailDetail", x => x.OrdreTravailDetailId);
                    table.ForeignKey(
                        name: "FK_OrdreTravailDetail_OrdreTravail_OrdreTravailId",
                        column: x => x.OrdreTravailId,
                        principalTable: "OrdreTravail",
                        principalColumn: "OrdreTravailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeShift = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleShift = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JourSemaine = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: true),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shift_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bus_SocieteId",
                table: "Bus",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_CircuitPointCollecte_CircuitId",
                table: "CircuitPointCollecte",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdreTravailDetail_OrdreTravailId",
                table: "OrdreTravailDetail",
                column: "OrdreTravailId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_SocieteId",
                table: "Shift",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "CircuitPointCollecte");

            migrationBuilder.DropTable(
                name: "OrdreTravailDetail");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropColumn(
                name: "CodePCArrivee",
                table: "Circuit");

            migrationBuilder.DropColumn(
                name: "CodePCDepart",
                table: "Circuit");

            migrationBuilder.DropColumn(
                name: "Couleur",
                table: "Circuit");

            migrationBuilder.DropColumn(
                name: "DistanceKm",
                table: "Circuit");

            migrationBuilder.DropColumn(
                name: "DureeMinutes",
                table: "Circuit");
        }
    }
}
