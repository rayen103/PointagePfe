using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationCodeAndApprovalToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-065f-68e8-6971b5106e05"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-0df3-b7ca-57edec253f6c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-1053-7dde-4864132372f7"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-1511-589b-db784232347f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-19e1-afdb-1095b418ca95"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-2f10-ac4a-2d6230773ae0"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-41bc-0f46-80221b8c2b2d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-43bc-8dbc-10b59d30b84a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-4c40-7f24-469caa955e33"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-4f5f-b3d0-0eac2f971b13"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-63f6-800f-6a17f1f83b33"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-67f9-c2ea-c07b6a4e92e6"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-705d-029a-454d7e8c1d43"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-773a-f716-2907988143d1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-7adf-4b01-0babe653ba12"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-7fc8-044d-5418e877cab9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-8612-ae2a-48d8bf2cd5ad"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-93b9-2b55-f05571446e5c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-a258-a686-68abc6f0e942"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-a9b2-5a33-876c0939e90f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-cb2d-2fc5-f9c45162ffd7"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-d0cb-4156-7849d24ddfe2"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-f2b1-5621-f719369727b1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d52-5dc8-f8c1-e69f-814161ec55bc"));

            migrationBuilder.AddColumn<string>(
                name: "ApprovalToken",
                table: "Utilisateur",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Utilisateur",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("01a03d89-d630-0084-c251-2eb3ec1e4a02"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-037d-fdaa-86f4f0b2c8df"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-0cd7-a4e9-aa3a464a88d5"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-15cc-76fa-7f226b444e34"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-1600-134d-dfffa1e5d6a1"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-1688-d7c4-3464b0812bf9"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-16f0-ebf2-2f6b27c3870b"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-2027-7bf3-b56f86f735d5"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-28ec-5417-ee113f2757c9"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-4a2a-4da4-795e9ea03f1d"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-71f8-1106-0e8a79dc370a"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-7d2a-771a-234fb1ced832"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-8acf-0f32-eaac62a31418"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-973d-6cbd-cd7d0cf3f3fd"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-9ab3-2518-d76b5bd3758c"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-a056-a3d1-ab351410f5eb"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-a0d4-62d8-5e1df146bbdb"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-a321-dba1-9cc68236f2ab"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-beba-ccbe-c352dd771190"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-dce8-9ff7-71085e865dbb"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-dcfb-563c-1f423864bf88"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-ddd7-415f-25d0d3a02b64"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-e776-65b8-e0ae245dff16"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d89-d630-e982-9279-8929336c3000"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });

            migrationBuilder.UpdateData(
                table: "Utilisateur",
                keyColumn: "UtilisateurId",
                keyValue: new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0"),
                columns: new[] { "ApprovalToken", "VerificationCode" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-0084-c251-2eb3ec1e4a02"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-037d-fdaa-86f4f0b2c8df"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-0cd7-a4e9-aa3a464a88d5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-15cc-76fa-7f226b444e34"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-1600-134d-dfffa1e5d6a1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-1688-d7c4-3464b0812bf9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-16f0-ebf2-2f6b27c3870b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-2027-7bf3-b56f86f735d5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-28ec-5417-ee113f2757c9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-4a2a-4da4-795e9ea03f1d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-71f8-1106-0e8a79dc370a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-7d2a-771a-234fb1ced832"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-8acf-0f32-eaac62a31418"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-973d-6cbd-cd7d0cf3f3fd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-9ab3-2518-d76b5bd3758c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-a056-a3d1-ab351410f5eb"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-a0d4-62d8-5e1df146bbdb"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-a321-dba1-9cc68236f2ab"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-beba-ccbe-c352dd771190"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-dce8-9ff7-71085e865dbb"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-dcfb-563c-1f423864bf88"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-ddd7-415f-25d0d3a02b64"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-e776-65b8-e0ae245dff16"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("01a03d89-d630-e982-9279-8929336c3000"));

            migrationBuilder.DropColumn(
                name: "ApprovalToken",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Utilisateur");

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("01a03d52-5dc8-065f-68e8-6971b5106e05"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-0df3-b7ca-57edec253f6c"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-1053-7dde-4864132372f7"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-1511-589b-db784232347f"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-19e1-afdb-1095b418ca95"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-2f10-ac4a-2d6230773ae0"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-41bc-0f46-80221b8c2b2d"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-43bc-8dbc-10b59d30b84a"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-4c40-7f24-469caa955e33"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-4f5f-b3d0-0eac2f971b13"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-63f6-800f-6a17f1f83b33"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-67f9-c2ea-c07b6a4e92e6"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-705d-029a-454d7e8c1d43"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-773a-f716-2907988143d1"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-7adf-4b01-0babe653ba12"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-7fc8-044d-5418e877cab9"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-8612-ae2a-48d8bf2cd5ad"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-93b9-2b55-f05571446e5c"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-a258-a686-68abc6f0e942"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-a9b2-5a33-876c0939e90f"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-cb2d-2fc5-f9c45162ffd7"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-d0cb-4156-7849d24ddfe2"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-f2b1-5621-f719369727b1"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("01a03d52-5dc8-f8c1-e69f-814161ec55bc"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });
        }
    }
}
