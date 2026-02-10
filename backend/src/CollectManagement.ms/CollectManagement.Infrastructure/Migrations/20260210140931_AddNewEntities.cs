using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Circuit",
                columns: table => new
                {
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeCircuit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleCircuit = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circuit", x => x.CircuitId);
                    table.ForeignKey(
                        name: "FK_Circuit_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employe",
                columns: table => new
                {
                    EmployeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Matricule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RFID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeCircuit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodePointCollecte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeShift = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CodeGouvernorat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeRegion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employe", x => x.EmployeId);
                    table.ForeignKey(
                        name: "FK_Employe_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeEquipe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleEquipe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodeClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeEntrepot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeTarif = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeFournisseur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CodeVehicule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipe", x => x.EquipeId);
                    table.ForeignKey(
                        name: "FK_Equipe_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdreTravail",
                columns: table => new
                {
                    OrdreTravailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroOrdreTravail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroChantier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumeroBonCommande = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeEquipe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EtatOT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Montant = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "date", nullable: true),
                    NumeroConvention = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeVehicule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Libelle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdreTravail", x => x.OrdreTravailId);
                    table.ForeignKey(
                        name: "FK_OrdreTravail_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointCollecte",
                columns: table => new
                {
                    PointCollecteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodePointCollecte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibellePointCollecte = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    CodeGouvernorat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeRegion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointCollecte", x => x.PointCollecteId);
                    table.ForeignKey(
                        name: "FK_PointCollecte_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rattachement",
                columns: table => new
                {
                    RattachementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroRattachement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Exercice = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateRattachement = table.Column<DateTime>(type: "date", nullable: false),
                    NumeroChantier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Cout = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nature = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HeureDebut = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    HeureFin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Emplacement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCloture = table.Column<DateTime>(type: "date", nullable: true),
                    Remarque = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rattachement", x => x.RattachementId);
                    table.ForeignKey(
                        name: "FK_Rattachement_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Circuit_SocieteId",
                table: "Circuit",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Employe_SocieteId",
                table: "Employe",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_SocieteId",
                table: "Equipe",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdreTravail_SocieteId",
                table: "OrdreTravail",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_PointCollecte_SocieteId",
                table: "PointCollecte",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Rattachement_SocieteId",
                table: "Rattachement",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circuit");

            migrationBuilder.DropTable(
                name: "Employe");

            migrationBuilder.DropTable(
                name: "Equipe");

            migrationBuilder.DropTable(
                name: "OrdreTravail");

            migrationBuilder.DropTable(
                name: "PointCollecte");

            migrationBuilder.DropTable(
                name: "Rattachement");
        }
    }
}
