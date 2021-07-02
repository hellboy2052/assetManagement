using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class FixSeedData_UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "c4eb55e9-3b2d-4643-b6e0-e1cc1ad47d6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "c4eb9cb8-103b-45c7-ad87-4c3c98dcd0ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "2ca58332-b8ea-4e5a-acc8-578c3ed4276e", "AQAAAAEAACcQAAAAELQBDpkNK+8vgvLYuHSFysNDWCM3dHmixdNEMZ+QW1RdkxYdr/fCFrXp7zrpBbYsZQ==", "0d79f818-6640-42ea-bc37-385514ba3e6e", "Staff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "ad95f059-7f2b-4b3a-851c-d8cedba50191", "AQAAAAEAACcQAAAAEHP+vEnceS/XMTlV6ezF/4sjhTt33XsTZkJgmx34iSL7pqn9bAeGnjHOZ5v5trrDkg==", "1ba9d3cf-89ed-4f30-9350-982ca393f090", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "4d246c6e-5921-467d-b81e-226189179d46", "AQAAAAEAACcQAAAAEAZbCkzucBMji1BsiyFBqp4UQ8aojP6Lagx3jtYYIKYTMxRf1TVV6Uvc1e0/HOXxBA==", "23e73f04-8c96-458a-bdf6-4d32cc562063", "Staff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
