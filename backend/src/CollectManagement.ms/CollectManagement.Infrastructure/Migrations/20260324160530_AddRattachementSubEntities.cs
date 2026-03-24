using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRattachementSubEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RattachementArticle",
                columns: table => new
                {
                    RattachementArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RattachementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeArticle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Quantite = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    PrixRevient = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TauxTVA = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CodeUnite = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CodeEntrepot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TypeRattachement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumeroBonLivraison = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateBonLivraison = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RattachementArticle", x => x.RattachementArticleId);
                    table.ForeignKey(
                        name: "FK_RattachementArticle_Rattachement_RattachementId",
                        column: x => x.RattachementId,
                        principalTable: "Rattachement",
                        principalColumn: "RattachementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RattachementArticle_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RattachementEmploye",
                columns: table => new
                {
                    RattachementEmployeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RattachementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Matricule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomPrenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDebut = table.Column<DateTime>(type: "date", nullable: true),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: true),
                    DateFin = table.Column<DateTime>(type: "date", nullable: true),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: true),
                    NombreHeure = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Cout = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CoutGlobal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TypeRattachement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RattachementEmploye", x => x.RattachementEmployeId);
                    table.ForeignKey(
                        name: "FK_RattachementEmploye_Rattachement_RattachementId",
                        column: x => x.RattachementId,
                        principalTable: "Rattachement",
                        principalColumn: "RattachementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RattachementEmploye_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RattachementArticle_RattachementId",
                table: "RattachementArticle",
                column: "RattachementId");

            migrationBuilder.CreateIndex(
                name: "IX_RattachementArticle_SocieteId",
                table: "RattachementArticle",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_RattachementEmploye_RattachementId",
                table: "RattachementEmploye",
                column: "RattachementId");

            migrationBuilder.CreateIndex(
                name: "IX_RattachementEmploye_SocieteId",
                table: "RattachementEmploye",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RattachementArticle");

            migrationBuilder.DropTable(
                name: "RattachementEmploye");
        }
    }
}
