using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase2CompanySiteReseauSplit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Adresse",
                table: "Societe",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePostal",
                table: "Societe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Initiales",
                table: "Societe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pays",
                table: "Societe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rc",
                table: "Societe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tva",
                table: "Societe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ville",
                table: "Societe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reseau",
                columns: table => new
                {
                    ReseauId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    GmtPlus = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Rayon = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TimeToleranceMinute = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reseau", x => x.ReseauId);
                    table.ForeignKey(
                        name: "FK_Reseau_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LibelleSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Siege = table.Column<bool>(type: "bit", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Rayon = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TimeMinute = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.SiteId);
                    table.ForeignKey(
                        name: "FK_Site_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Societe",
                keyColumn: "SocieteId",
                keyValue: new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0"),
                columns: new[] { "CodePostal", "Initiales", "Pays", "Rc", "Tva", "Ville" },
                values: new object[] { null, "CST", null, "RC-CST-001", "TVA-CST-001", null });

            migrationBuilder.CreateIndex(
                name: "IX_Reseau_SocieteId",
                table: "Reseau",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_SocieteId",
                table: "Site",
                column: "SocieteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reseau");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropColumn(
                name: "CodePostal",
                table: "Societe");

            migrationBuilder.DropColumn(
                name: "Initiales",
                table: "Societe");

            migrationBuilder.DropColumn(
                name: "Pays",
                table: "Societe");

            migrationBuilder.DropColumn(
                name: "Rc",
                table: "Societe");

            migrationBuilder.DropColumn(
                name: "Tva",
                table: "Societe");

            migrationBuilder.DropColumn(
                name: "Ville",
                table: "Societe");

            migrationBuilder.AlterColumn<string>(
                name: "Adresse",
                table: "Societe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
