using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-1397-9c28-ab2b0c0a4e89"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-2642-8ea1-1daf4b26c939"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-2f79-d890-b5a115c24800"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-35db-15f8-7ca14672294e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-3d07-f19e-58255548ca89"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-3d80-7210-1d349a514047"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-43fb-6e11-4abab6c329e5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-6522-6dd6-b98251344564"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-656b-09dc-1bde075903bd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-7780-2a9e-a64ce11c3965"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-7901-a7c3-6e7616526216"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-826d-541e-a4c2f487d200"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-8836-b18e-540c360b0eda"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-915d-5b36-fbf778b7cc48"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-a75b-6399-9d7b685b63b3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-af97-0f72-6435342f638b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-ce61-0d6f-e628169f22bb"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-d62b-b678-cc98c253976e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-dadf-4305-f0cd131ed86f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-e663-2ecc-26c75408ba6a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-ec72-eedf-ffa9b79e3304"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-ef7f-ab24-deb848471e1c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-fa20-5448-36c32fcc9de4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e508f-bd5a-fbc9-e1a2-9899b713733f"));

            migrationBuilder.CreateTable(
                name: "ReportLayout",
                columns: table => new
                {
                    ReportLayoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConfigJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SocieteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsererPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierPar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLayout", x => x.ReportLayoutId);
                    table.ForeignKey(
                        name: "FK_ReportLayout_Societe_SocieteId",
                        column: x => x.SocieteId,
                        principalTable: "Societe",
                        principalColumn: "SocieteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019e5a15-28f3-0831-d237-54336c6f9aed"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-1fe8-ecc1-bbcd5e5bc220"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-2146-0ace-b311c9afc436"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-26fa-52f9-53546ccd955b"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-3779-c619-776d451f5f00"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-3b87-36b3-003afbb50bcc"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-3d79-4ef8-4e66297e457e"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-52ac-1001-f0f10d980a15"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-5471-397d-a888b3c81b97"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-6e35-afa3-decb1fbe8416"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-6fe1-c436-d858e86c3c08"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-7d52-4ba2-3c7f260c1cf1"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-7ff4-9a7b-61eaeda6c6e9"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-8e16-a6e0-7f9583f9a656"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-9533-697b-0fb0a8c2092c"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-9a12-35bd-657f354e8c61"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-a204-88f6-134826736c13"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-a9b9-1fca-4a12547b5651"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-c1d8-9d96-9ac36a8cfc42"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-c9aa-ae58-405dd473b463"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-d146-c65b-f9cdcb62b126"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-dd24-f828-65edf521f802"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-f1b0-15cf-1ee9f5a4e16e"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e5a15-28f3-fa87-c753-191c4bd2dc0c"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportLayout_SocieteId_ReportType",
                table: "ReportLayout",
                columns: new[] { "SocieteId", "ReportType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportLayout");

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-0831-d237-54336c6f9aed"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-1fe8-ecc1-bbcd5e5bc220"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-2146-0ace-b311c9afc436"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-26fa-52f9-53546ccd955b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-3779-c619-776d451f5f00"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-3b87-36b3-003afbb50bcc"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-3d79-4ef8-4e66297e457e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-52ac-1001-f0f10d980a15"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-5471-397d-a888b3c81b97"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-6e35-afa3-decb1fbe8416"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-6fe1-c436-d858e86c3c08"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-7d52-4ba2-3c7f260c1cf1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-7ff4-9a7b-61eaeda6c6e9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-8e16-a6e0-7f9583f9a656"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-9533-697b-0fb0a8c2092c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-9a12-35bd-657f354e8c61"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-a204-88f6-134826736c13"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-a9b9-1fca-4a12547b5651"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-c1d8-9d96-9ac36a8cfc42"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-c9aa-ae58-405dd473b463"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-d146-c65b-f9cdcb62b126"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-dd24-f828-65edf521f802"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-f1b0-15cf-1ee9f5a4e16e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e5a15-28f3-fa87-c753-191c4bd2dc0c"));

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019e508f-bd5a-1397-9c28-ab2b0c0a4e89"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-2642-8ea1-1daf4b26c939"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-2f79-d890-b5a115c24800"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-35db-15f8-7ca14672294e"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-3d07-f19e-58255548ca89"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-3d80-7210-1d349a514047"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-43fb-6e11-4abab6c329e5"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-6522-6dd6-b98251344564"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-656b-09dc-1bde075903bd"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-7780-2a9e-a64ce11c3965"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-7901-a7c3-6e7616526216"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-826d-541e-a4c2f487d200"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-8836-b18e-540c360b0eda"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-915d-5b36-fbf778b7cc48"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-a75b-6399-9d7b685b63b3"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-af97-0f72-6435342f638b"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-ce61-0d6f-e628169f22bb"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-d62b-b678-cc98c253976e"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-dadf-4305-f0cd131ed86f"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-e663-2ecc-26c75408ba6a"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-ec72-eedf-ffa9b79e3304"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-ef7f-ab24-deb848471e1c"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-fa20-5448-36c32fcc9de4"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e508f-bd5a-fbc9-e1a2-9899b713733f"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });
        }
    }
}
