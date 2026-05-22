using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnableAdminAndSocieteSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Societe",
                columns: new[] { "SocieteId", "Adresse", "Capital", "CodeSociete", "DateInsertion", "DateModification", "DateOverture", "Email", "Fax1", "Fax2", "InsererPar", "LogoPath", "MatriculeFiscal", "ModifierPar", "Nom", "Rne", "Telephone1", "Telephone2" },
                values: new object[] { new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0"), null, 0m, "CST", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@cst.tn", null, null, null, null, "MF-CST-001", null, "CST", "RNE-CST-001", null, null });

            migrationBuilder.InsertData(
                table: "Utilisateur",
                columns: new[] { "UtilisateurId", "DateInsertion", "DateModification", "Email", "InsererPar", "IsActive", "ModifierPar", "Nom", "NomUtilisateur", "Password", "Prenom", "RoleUtilisateurId", "SocieteId" },
                values: new object[] { new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0"), null, null, "admin@cst.tn", null, true, null, "Admin", "admin", "E2CF9A6F4CFCA46F74FC0E4CF7A5B278D3C20D9178E0DB936DBB3CF8E614C89E4D1C33229F39A457014D2D581CAA3DCE7F49C53803A176A4F891A9EB1D5A34BA", "CST", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Utilisateur",
                keyColumn: "UtilisateurId",
                keyValue: new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0"));

            migrationBuilder.DeleteData(
                table: "Societe",
                keyColumn: "SocieteId",
                keyValue: new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0"));
        }
    }
}
