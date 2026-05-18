using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportReferenceSatellites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeChauffeur",
                table: "Bus",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chauffeur",
                columns: table => new
                {
                    ChauffeurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeChauffeur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RFIDChauffeur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Externe = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chauffeur", x => x.ChauffeurId);
                    table.ForeignKey(
                        name: "FK_Chauffeur_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gouvernorat",
                columns: table => new
                {
                    GouvernoratId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeGouvernorat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleGouvernorat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gouvernorat", x => x.GouvernoratId);
                    table.ForeignKey(
                        name: "FK_Gouvernorat_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modem",
                columns: table => new
                {
                    ModemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IMEI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelModem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroSim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modem", x => x.ModemId);
                    table.ForeignKey(
                        name: "FK_Modem_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeRegion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleRegion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodeGouvernorat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Region_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chauffeur_SocieteId",
                table: "Chauffeur",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Gouvernorat_SocieteId",
                table: "Gouvernorat",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Modem_SocieteId",
                table: "Modem",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_SocieteId",
                table: "Region",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chauffeur");

            migrationBuilder.DropTable(
                name: "Gouvernorat");

            migrationBuilder.DropTable(
                name: "Modem");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropColumn(
                name: "CodeChauffeur",
                table: "Bus");
        }
    }
}
