using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleUtilisateurAndAddUtilisateurSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-04d9-290b-f069588b3a0e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-0b3c-974e-2609dedd2754"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-1453-f8e6-a1a0aff5c9f5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-29eb-1666-5220e24850b4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-2eca-409e-a68be47159e3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-6074-4286-1c711ec287fd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-6121-a9f4-87aab8c9fa12"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-6665-8d51-734f1ffa6ea8"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-9078-0834-21a976d58a67"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-90ed-028d-ec3b8b0b43fa"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-9108-3152-1a47199d8702"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-9505-7268-668d43b7d8b2"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-9fca-5dd5-f538c5bca9ac"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-a0dd-2648-ea896c697b81"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-b3ff-200c-aacab83a75a9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-b583-191e-5b6f27dc153d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-b5b3-8110-7e5eaf3fa08c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-c3bf-6c84-a108ae99eff8"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-c6fa-0af7-60d179ec7a31"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-c728-625c-09db54209845"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-ce25-afca-9d8b206cf546"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-d4be-3e7f-b4c9f5059c6f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-ea08-d444-001393ee50f4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e503c-a5ce-f91e-48e1-6b6f3c0b19bb"));

            migrationBuilder.AddColumn<Guid>(
                name: "SocieteId",
                table: "RoleUtilisateur",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UtilisateurSite",
                columns: table => new
                {
                    UtilisateurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurSite", x => new { x.UtilisateurId, x.SiteId });
                    table.ForeignKey(
                        name: "FK_UtilisateurSite_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilisateurSite_Utilisateur_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateur",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019e5065-e327-0930-135c-4e6b70870bd0"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-0dd8-e283-3ca630c73458"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-0f7a-31ac-7383772b901c"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-2673-5615-c7fe32a4a41c"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-33d5-95e0-30cf429da5ce"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-400a-551d-b6bcf384e68e"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-40a2-8178-9b64bbc2c7c8"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-4781-d6b6-f9675e924174"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-51c6-2689-b96c31cd6869"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-52ca-a3fc-417b0de18491"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-5b4c-6be2-6bbdc1b47079"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-6212-734d-2f967c9d4319"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-809a-6df3-17738bce5864"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-8918-a75b-723de9d62126"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-8e6b-d8d3-14a128e0c510"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-9dab-2f56-5cfc8755fc12"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-ab24-93ca-772b96003f6d"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-b84c-3034-a488958fdc05"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-b965-6563-221bd611cd04"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-bdc6-f11a-0dcf6b479589"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-cf32-e510-5f0bb007f345"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-d409-25a3-3f0e60a408ab"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-e3a7-48f1-f64d534d1d5e"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5065-e327-f572-f17d-9eebacd54dbd"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUtilisateur_SocieteId",
                table: "RoleUtilisateur",
                column: "SocieteId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurSite_SiteId",
                table: "UtilisateurSite",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUtilisateur_Societe_SocieteId",
                table: "RoleUtilisateur",
                column: "SocieteId",
                principalTable: "Societe",
                principalColumn: "SocieteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUtilisateur_Societe_SocieteId",
                table: "RoleUtilisateur");

            migrationBuilder.DropTable(
                name: "UtilisateurSite");

            migrationBuilder.DropIndex(
                name: "IX_RoleUtilisateur_SocieteId",
                table: "RoleUtilisateur");

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-0930-135c-4e6b70870bd0"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-0dd8-e283-3ca630c73458"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-0f7a-31ac-7383772b901c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-2673-5615-c7fe32a4a41c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-33d5-95e0-30cf429da5ce"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-400a-551d-b6bcf384e68e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-40a2-8178-9b64bbc2c7c8"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-4781-d6b6-f9675e924174"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-51c6-2689-b96c31cd6869"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-52ca-a3fc-417b0de18491"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-5b4c-6be2-6bbdc1b47079"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-6212-734d-2f967c9d4319"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-809a-6df3-17738bce5864"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-8918-a75b-723de9d62126"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-8e6b-d8d3-14a128e0c510"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-9dab-2f56-5cfc8755fc12"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-ab24-93ca-772b96003f6d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-b84c-3034-a488958fdc05"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-b965-6563-221bd611cd04"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-bdc6-f11a-0dcf6b479589"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-cf32-e510-5f0bb007f345"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-d409-25a3-3f0e60a408ab"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-e3a7-48f1-f64d534d1d5e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5065-e327-f572-f17d-9eebacd54dbd"));

            migrationBuilder.DropColumn(
                name: "SocieteId",
                table: "RoleUtilisateur");

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019e503c-a5ce-04d9-290b-f069588b3a0e"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-0b3c-974e-2609dedd2754"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-1453-f8e6-a1a0aff5c9f5"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-29eb-1666-5220e24850b4"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-2eca-409e-a68be47159e3"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-6074-4286-1c711ec287fd"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-6121-a9f4-87aab8c9fa12"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-6665-8d51-734f1ffa6ea8"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-9078-0834-21a976d58a67"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-90ed-028d-ec3b8b0b43fa"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-9108-3152-1a47199d8702"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-9505-7268-668d43b7d8b2"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-9fca-5dd5-f538c5bca9ac"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-a0dd-2648-ea896c697b81"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-b3ff-200c-aacab83a75a9"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-b583-191e-5b6f27dc153d"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-b5b3-8110-7e5eaf3fa08c"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-c3bf-6c84-a108ae99eff8"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-c6fa-0af7-60d179ec7a31"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-c728-625c-09db54209845"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-ce25-afca-9d8b206cf546"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-d4be-3e7f-b4c9f5059c6f"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-ea08-d444-001393ee50f4"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e503c-a5ce-f91e-48e1-6b6f3c0b19bb"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });
        }
    }
}
