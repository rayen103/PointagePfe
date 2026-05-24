using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGouvernoratSeedAndModemUniqueIMEI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Gouvernorat', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Gouvernorat] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Gouvernorat_IsActive] DEFAULT CAST(1 AS bit);
END;

IF COL_LENGTH('dbo.Chauffeur', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Chauffeur] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Chauffeur_IsActive] DEFAULT CAST(1 AS bit);
END;

IF COL_LENGTH('dbo.Modem', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Modem] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Modem_IsActive] DEFAULT CAST(1 AS bit);
END;

IF COL_LENGTH('dbo.Region', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Region] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Region_IsActive] DEFAULT CAST(1 AS bit);
END;

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Gouvernorat_Region_RegionId')
BEGIN
    ALTER TABLE [dbo].[Gouvernorat] DROP CONSTRAINT [FK_Gouvernorat_Region_RegionId];
END;

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Gouvernorat_RegionId' AND object_id = OBJECT_ID('dbo.Gouvernorat'))
BEGIN
    DROP INDEX [IX_Gouvernorat_RegionId] ON [dbo].[Gouvernorat];
END;

IF COL_LENGTH('dbo.Gouvernorat', 'RegionId') IS NOT NULL
BEGIN
    ALTER TABLE [dbo].[Gouvernorat] DROP COLUMN [RegionId];
END;
");

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

            migrationBuilder.CreateIndex(
                name: "IX_Modem_IMEI",
                table: "Modem",
                column: "IMEI",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Modem_IMEI",
                table: "Modem");

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
        }
    }
}
