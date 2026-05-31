using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingCodeGouvernoratColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-0d23-df1e-43cf6481f183"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-19e2-6e9f-4a4768be5d5a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-2b63-d017-d1651d6b8b70"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-49e3-eaff-edd74d44a9e4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-4ab7-c331-7e1460f2138d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-4f70-e488-f53c2529892b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-6019-b4e2-9c62eb46bc78"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-6717-b5dc-59245eca27fd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-68ce-15f4-a2fd0d448260"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-6901-3361-a1ff8853f401"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-7fe7-5347-1b0a9dbd190f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-83f8-df74-e592fd85e9df"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-a432-9471-096404a7ee40"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-af52-ad5f-bf2cfa984aa7"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-b8cb-33b0-acc548cd9576"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-c5ba-a955-45ffd0f6bb0e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-d234-7c87-8377e18e13b4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-d659-8fdf-ae1a0a787273"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-d8be-b131-3f5d176fe421"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-e753-378a-a816527dc00a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-e976-687f-6f58c2b585e7"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-ef83-d5d5-7a24d81cac99"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-f6dd-6588-a643a4803d14"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e6f-b88f-fa2a-51a7-7f0bcb22c928"));

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019e7e73-3b47-2799-a27d-4dbdf49342d5"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-4e33-8457-6426a40aeccd"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-4eb2-041b-4a73ec70c2c1"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-6820-6288-f9f11b0c80c3"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-6d1a-7ba3-6f3bab2f2d1a"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-7455-0305-0c0dc820b16c"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-7dde-8d07-35d32a12b821"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-7e20-f1d4-0090c785bda9"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-94d3-989c-e0ec565a2306"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-9775-c8e9-8982dc5adcd5"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-a084-ca5f-318fe44a68dd"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-a2c9-aed2-f12d63d174d3"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-be27-c26c-f82f448006c0"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-bfc8-7417-c2934b0bb279"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-c103-3006-aec4abaa8529"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-c4ea-8061-26d6bfd38f50"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-c8ba-27a6-84ae407bfd55"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-cb8c-3fe0-670abd3dedc3"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-d02a-71f2-cb472ecfe2a1"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-ec09-9384-dd5fd42321fd"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-f087-4f2e-9d73431534d5"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-f441-a957-ed2b0175c652"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-f542-698b-f1335699d9ae"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e73-3b47-f6a5-595f-4154896714f8"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-2799-a27d-4dbdf49342d5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-4e33-8457-6426a40aeccd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-4eb2-041b-4a73ec70c2c1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-6820-6288-f9f11b0c80c3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-6d1a-7ba3-6f3bab2f2d1a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-7455-0305-0c0dc820b16c"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-7dde-8d07-35d32a12b821"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-7e20-f1d4-0090c785bda9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-94d3-989c-e0ec565a2306"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-9775-c8e9-8982dc5adcd5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-a084-ca5f-318fe44a68dd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-a2c9-aed2-f12d63d174d3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-be27-c26c-f82f448006c0"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-bfc8-7417-c2934b0bb279"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-c103-3006-aec4abaa8529"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-c4ea-8061-26d6bfd38f50"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-c8ba-27a6-84ae407bfd55"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-cb8c-3fe0-670abd3dedc3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-d02a-71f2-cb472ecfe2a1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-ec09-9384-dd5fd42321fd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-f087-4f2e-9d73431534d5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-f441-a957-ed2b0175c652"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-f542-698b-f1335699d9ae"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019e7e73-3b47-f6a5-595f-4154896714f8"));

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019e7e6f-b88f-0d23-df1e-43cf6481f183"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-19e2-6e9f-4a4768be5d5a"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-2b63-d017-d1651d6b8b70"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-49e3-eaff-edd74d44a9e4"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-4ab7-c331-7e1460f2138d"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-4f70-e488-f53c2529892b"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-6019-b4e2-9c62eb46bc78"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-6717-b5dc-59245eca27fd"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-68ce-15f4-a2fd0d448260"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-6901-3361-a1ff8853f401"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-7fe7-5347-1b0a9dbd190f"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-83f8-df74-e592fd85e9df"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-a432-9471-096404a7ee40"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-af52-ad5f-bf2cfa984aa7"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-b8cb-33b0-acc548cd9576"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-c5ba-a955-45ffd0f6bb0e"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-d234-7c87-8377e18e13b4"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-d659-8fdf-ae1a0a787273"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-d8be-b131-3f5d176fe421"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-e753-378a-a816527dc00a"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-e976-687f-6f58c2b585e7"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-ef83-d5d5-7a24d81cac99"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-f6dd-6588-a643a4803d14"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019e7e6f-b88f-fa2a-51a7-7f0bcb22c928"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });
        }
    }
}
