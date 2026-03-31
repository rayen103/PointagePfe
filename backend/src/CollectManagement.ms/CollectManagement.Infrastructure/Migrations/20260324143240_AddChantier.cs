using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChantier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chantier",
                columns: table => new
                {
                    ChantierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroChantier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleChantier = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodeClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MontantHT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MontantTTC = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Nature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chantier", x => x.ChantierId);
                    table.ForeignKey(
                        name: "FK_Chantier_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chantier_SocieteId",
                table: "Chantier",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chantier");
        }
    }
}
