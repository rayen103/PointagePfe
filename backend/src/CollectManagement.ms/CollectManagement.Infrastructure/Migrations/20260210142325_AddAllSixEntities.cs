using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllSixEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check and create Employe table (already exists in database)
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employe]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [Employe] (
                        [EmployeId] uniqueidentifier NOT NULL,
                        [Matricule] nvarchar(50) NOT NULL,
                        [RFID] nvarchar(50) NULL,
                        [Nom] nvarchar(100) NOT NULL,
                        [Prenom] nvarchar(100) NOT NULL,
                        [CodeCircuit] nvarchar(50) NULL,
                        [CodePointCollecte] nvarchar(50) NULL,
                        [CodeShift] nvarchar(50) NULL,
                        [Adresse] nvarchar(255) NULL,
                        [CodeGouvernorat] nvarchar(50) NULL,
                        [CodeRegion] nvarchar(50) NULL,
                        [SocieteId] uniqueidentifier NOT NULL,
                        [InsererPar] nvarchar(max) NULL,
                        [DateInsertion] datetime2 NULL,
                        [ModifierPar] nvarchar(max) NULL,
                        [DateModification] datetime2 NULL,
                        CONSTRAINT [PK_Employe] PRIMARY KEY ([EmployeId]),
                        CONSTRAINT [FK_Employe_Societe_SocieteId] FOREIGN KEY ([SocieteId]) REFERENCES [Societe] ([SocieteId]) ON DELETE NO ACTION
                    );
                    CREATE INDEX [IX_Employe_SocieteId] ON [Employe] ([SocieteId]);
                END
            ");
            
            // Create Circuit table
            migrationBuilder.CreateTable(
                name: "Circuit",
                columns: table => new
                {
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeCircuit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleCircuit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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

            // Create PointCollecte table
            migrationBuilder.CreateTable(
                name: "PointCollecte",
                columns: table => new
                {
                    PointCollecteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodePointCollecte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibellePointCollecte = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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

            // Create Equipe table
            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeEquipe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleEquipe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeEntrepot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeTarif = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeFournisseur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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

            // Create OrdreTravail table
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
                    Montant = table.Column<decimal>(type: "decimal(18,3)", nullable: false, defaultValue: 0m),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroConvention = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeVehicule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Libelle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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

            // Create Rattachement table
            migrationBuilder.CreateTable(
                name: "Rattachement",
                columns: table => new
                {
                    RattachementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroRattachement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Exercice = table.Column<int>(type: "int", nullable: false),
                    DateRattachement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroChantier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Cout = table.Column<decimal>(type: "decimal(18,3)", nullable: false, defaultValue: 0m),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nature = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: true),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: true),
                    Emplacement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCloture = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Circuit_SocieteId",
                table: "Circuit",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_PointCollecte_SocieteId",
                table: "PointCollecte",
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
                name: "IX_Rattachement_SocieteId",
                table: "Rattachement",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Circuit");
            migrationBuilder.DropTable(name: "PointCollecte");
            migrationBuilder.DropTable(name: "Equipe");
            migrationBuilder.DropTable(name: "OrdreTravail");
            migrationBuilder.DropTable(name: "Rattachement");
            migrationBuilder.DropTable(name: "Employe");
        }
    }
}
