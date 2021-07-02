using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class TypeProperty_UserTable_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "8f556992-1f0e-4d59-bf81-099c74ccb1e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "5327db7e-6fe7-4d37-9259-8be54dc1706d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "81fce508-3a75-4f6e-9025-f3498bb89f72", "AQAAAAEAACcQAAAAEBmNjY+0vAFSLfHel2m85Ar4E4wrhmss87BaD0PgX9vaWKfhDnPZEP1irUOFotqyBQ==", "8f0d44ad-456f-4607-93c5-63f8f1f36c91", "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "6fd3f28b-8434-4782-8e5c-06827af4c585", "AQAAAAEAACcQAAAAEG6V2ePxILt3JNnDT4Cf0GdwoBYQsSum5+Biuw9Rqcl4kM0YjEUQeaVqAVTI7joNbA==", "35a0f516-635f-4472-9911-db44ceec503c", "c581eb54-d110-4177-b851-7a616d52abc8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "dbda7c24-cc8c-4143-9f27-38f1c52bf021", "AQAAAAEAACcQAAAAEErJ3d25cd/dRQOlACmPrmNSWVndyLBXQF1OKBF5mr84nEp+L3tSyfVHBKBIXP5y8g==", "d5f2c03c-d02c-47fa-85fe-f49286e366ce", "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "df742001-cd77-4eae-a577-a5e1621996f3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "31773a40-ddf0-451e-bc37-a7b95a740d4b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1df19b1a-b672-49da-8c1a-4da8a5c08a49", "AQAAAAEAACcQAAAAEEzwQs3bwqFjsALzhTCIA8w800Bw+rvm2W9YCUgdqilkFP6Lm4CZZdHSqHzXsxFRkw==", "03ceca8c-d4f4-456b-9132-16a1f54afdb8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5a45468-1fb6-47ff-ae81-8d9125f5acbf", "AQAAAAEAACcQAAAAEBCdbeGry8MGiXZpU7K8ZkS3xXhqNYm7J3oW34W8gYpywFRa2jn56ZTNukQgTrC72w==", "aa359501-fa6d-4691-876f-d9654379d510" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9630e8d4-4d4a-4a80-923f-5711fe3e6865", "AQAAAAEAACcQAAAAENNWjIe5VyCaEUaMHv0kgq5nwuvvitJtXRfd9G2r1VGg8Vj2jbPgXlRK37BUrPoZ6Q==", "238d2c2c-111d-4b96-bbf8-dd28ec83cb72" });
        }
    }
}
