using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusIdToChauffeurAndRemoveTypeEmployeFromEmploye : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TypeEmploye",
                table: "Employe");

            migrationBuilder.AddColumn<Guid>(
                name: "CircuitId",
                table: "PointCollecte",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BusId",
                table: "Chauffeur",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019ed6e6-0db7-0a4d-6e4b-484381fc4b8b"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-0c5e-a04a-da14b21bee8d"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-1178-292b-001fe869dc44"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-1bff-a2c1-feda6713e704"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-2de5-146b-d32144dc8d15"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-40ad-c634-92926a6dcf71"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-492a-4e6a-60bd1e57c5d9"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-6409-3952-adac5807a1ad"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-64d2-9455-1ace978c0656"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-6e87-765e-d00e09ee2ac5"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-7635-316e-408e81392354"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-904a-111f-87ba4971ee7f"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-9c79-47d0-98f866d6e2ce"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-9e4c-1fec-3f336e9778f3"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-a3ed-bd12-ef88c13846a8"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-aadf-2f5b-e0d3d715b0dd"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-b8d2-ffa1-558e2b244cd4"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-be8b-1423-99169eb6ec54"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-c41a-7898-223217b2a26a"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-d01c-6ad8-c557efb0d8e1"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-da13-2540-b97a0727adc1"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-db07-2e03-bb8459c229e0"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-e8e7-e143-e219469b8582"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-0db7-ed93-5f33-d06d6b23078f"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointCollecte_CircuitId",
                table: "PointCollecte",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_Chauffeur_BusId",
                table: "Chauffeur",
                column: "BusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chauffeur_Bus_BusId",
                table: "Chauffeur",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "BusId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PointCollecte_Circuit_CircuitId",
                table: "PointCollecte",
                column: "CircuitId",
                principalTable: "Circuit",
                principalColumn: "CircuitId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chauffeur_Bus_BusId",
                table: "Chauffeur");

            migrationBuilder.DropForeignKey(
                name: "FK_PointCollecte_Circuit_CircuitId",
                table: "PointCollecte");

            migrationBuilder.DropIndex(
                name: "IX_PointCollecte_CircuitId",
                table: "PointCollecte");

            migrationBuilder.DropIndex(
                name: "IX_Chauffeur_BusId",
                table: "Chauffeur");

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-0a4d-6e4b-484381fc4b8b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-0c5e-a04a-da14b21bee8d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-1178-292b-001fe869dc44"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-1bff-a2c1-feda6713e704"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-2de5-146b-d32144dc8d15"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-40ad-c634-92926a6dcf71"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-492a-4e6a-60bd1e57c5d9"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-6409-3952-adac5807a1ad"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-64d2-9455-1ace978c0656"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-6e87-765e-d00e09ee2ac5"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-7635-316e-408e81392354"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-904a-111f-87ba4971ee7f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-9c79-47d0-98f866d6e2ce"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-9e4c-1fec-3f336e9778f3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-a3ed-bd12-ef88c13846a8"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-aadf-2f5b-e0d3d715b0dd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-b8d2-ffa1-558e2b244cd4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-be8b-1423-99169eb6ec54"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-c41a-7898-223217b2a26a"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-d01c-6ad8-c557efb0d8e1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-da13-2540-b97a0727adc1"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-db07-2e03-bb8459c229e0"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-e8e7-e143-e219469b8582"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-0db7-ed93-5f33-d06d6b23078f"));

            migrationBuilder.DropColumn(
                name: "CircuitId",
                table: "PointCollecte");

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "Chauffeur");

            migrationBuilder.AddColumn<string>(
                name: "TypeEmploye",
                table: "Employe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "EmployeSimple");

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
    }
}
