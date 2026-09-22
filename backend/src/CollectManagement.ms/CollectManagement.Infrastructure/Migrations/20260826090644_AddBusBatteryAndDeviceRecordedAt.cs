using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusBatteryAndDeviceRecordedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-313a-a06c-edb5114d1ef2"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-379b-b7c6-a7eae62667b0"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-3a4e-1301-2578bb037299"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-3e91-39d7-df663cb8455e"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-4622-c615-fe61f74345c3"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-465a-fa89-32c17bae932d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-4edf-72ca-463f4dfa3247"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-52d7-edce-e37a6f2c0334"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-57da-2f4f-0d0fd5db9811"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-793f-38c0-19bc38c24e59"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-8be2-5869-bde0f44b3ace"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-9a32-25c6-abe83475ef30"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-9cd1-9373-733b992d452f"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-9e81-b9a2-69b5bd9d2dab"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-a18f-0e8a-f45be5cd62bd"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-abd9-2d0e-a953b17b15d4"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-bac1-b487-1d66ea245d68"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-bd8c-d4ac-6dc266bfb48b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-c04f-17d1-c134b4ef11ff"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-c855-1014-b19f9e181a0d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-cf70-4400-e58a76b42afc"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-d204-e41e-784755b9c57d"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-d510-e8f2-470f82022a6b"));

            migrationBuilder.DeleteData(
                table: "Gouvernorat",
                keyColumn: "GouvernoratId",
                keyValue: new Guid("019ed6e6-e92a-d6a0-3373-58f74e175fd6"));

            migrationBuilder.AddColumn<int>(
                name: "BatteryPercentage",
                table: "Bus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BatteryVoltage",
                table: "Bus",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeviceRecordedAtUtc",
                table: "Bus",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BatteryPercentage",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "BatteryVoltage",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DeviceRecordedAtUtc",
                table: "Bus");

            migrationBuilder.InsertData(
                table: "Gouvernorat",
                columns: new[] { "GouvernoratId", "CodeGouvernorat", "DateInsertion", "DateModification", "InsererPar", "IsActive", "LibelleGouvernorat", "ModifierPar", "SocieteId" },
                values: new object[,]
                {
                    { new Guid("019ed6e6-e92a-313a-a06c-edb5114d1ef2"), "13", null, null, null, true, "Ben Arous", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-379b-b7c6-a7eae62667b0"), "61", null, null, null, true, "Sfax", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-3a4e-1301-2578bb037299"), "82", null, null, null, true, "Médenine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-3e91-39d7-df663cb8455e"), "14", null, null, null, true, "Manouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-4622-c615-fe61f74345c3"), "52", null, null, null, true, "Monastir", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-465a-fa89-32c17bae932d"), "73", null, null, null, true, "Kebili", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-4edf-72ca-463f4dfa3247"), "22", null, null, null, true, "Zaghouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-52d7-edce-e37a6f2c0334"), "12", null, null, null, true, "Ariana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-57da-2f4f-0d0fd5db9811"), "83", null, null, null, true, "Tataouine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-793f-38c0-19bc38c24e59"), "71", null, null, null, true, "Gafsa", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-8be2-5869-bde0f44b3ace"), "23", null, null, null, true, "Bizerte", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-9a32-25c6-abe83475ef30"), "32", null, null, null, true, "Jendouba", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-9cd1-9373-733b992d452f"), "21", null, null, null, true, "Nabeul", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-9e81-b9a2-69b5bd9d2dab"), "51", null, null, null, true, "Sousse", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-a18f-0e8a-f45be5cd62bd"), "42", null, null, null, true, "Kasserine", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-abd9-2d0e-a953b17b15d4"), "41", null, null, null, true, "Kairouan", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-bac1-b487-1d66ea245d68"), "31", null, null, null, true, "Béja", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-bd8c-d4ac-6dc266bfb48b"), "53", null, null, null, true, "Mahdia", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-c04f-17d1-c134b4ef11ff"), "81", null, null, null, true, "Gabès", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-c855-1014-b19f9e181a0d"), "43", null, null, null, true, "Sidi Bouzid", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-cf70-4400-e58a76b42afc"), "33", null, null, null, true, "Le Kef", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-d204-e41e-784755b9c57d"), "11", null, null, null, true, "Tunis", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-d510-e8f2-470f82022a6b"), "34", null, null, null, true, "Siliana", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") },
                    { new Guid("019ed6e6-e92a-d6a0-3373-58f74e175fd6"), "72", null, null, null, true, "Tozeur", null, new Guid("018b1055-d0b7-de38-752f-1b18f580c2e0") }
                });
        }
    }
}
