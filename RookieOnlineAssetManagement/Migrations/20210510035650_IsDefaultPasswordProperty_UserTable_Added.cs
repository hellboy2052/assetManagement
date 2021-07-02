using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class IsDefaultPasswordProperty_UserTable_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultPassword",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "IsDisabled", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "308a0f9c-2744-45d1-96c6-3c2f624f284c", 0, "1df19b1a-b672-49da-8c1a-4da8a5c08a49", new DateTime(1996, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Nguyen Van", true, 3, true, new DateTime(2018, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Binh", "HN", false, null, null, null, "AQAAAAEAACcQAAAAEEzwQs3bwqFjsALzhTCIA8w800Bw+rvm2W9YCUgdqilkFP6Lm4CZZdHSqHzXsxFRkw==", null, false, "03ceca8c-d4f4-456b-9132-16a1f54afdb8", false, "binhnv1" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a", 0, "9630e8d4-4d4a-4a80-923f-5711fe3e6865", new DateTime(1993, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Tran Van", false, 2, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "An", "HCM", false, null, null, null, "AQAAAAEAACcQAAAAENNWjIe5VyCaEUaMHv0kgq5nwuvvitJtXRfd9G2r1VGg8Vj2jbPgXlRK37BUrPoZ6Q==", null, false, "238d2c2c-111d-4b96-bbf8-dd28ec83cb72", false, "antv" },
                    { "5f6f60e1-433b-4c02-8af2-46fb29be8568", 0, "b5a45468-1fb6-47ff-ae81-8d9125f5acbf", new DateTime(1999, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Nguyen Thuy", true, 1, new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "An", "HCM", false, null, null, null, "AQAAAAEAACcQAAAAEBCdbeGry8MGiXZpU7K8ZkS3xXhqNYm7J3oW34W8gYpywFRa2jn56ZTNukQgTrC72w==", null, false, "aa359501-fa6d-4691-876f-d9654379d510", false, "annt" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultPassword",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "27665e55-5151-48e8-87a3-8c1be21d86bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "fbb2e48d-baba-4069-9025-8348db8d87cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "IncrementId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10b528c5-e2a5-4c21-9645-fa87c16ea06e", 0, "AQAAAAEAACcQAAAAEH8/OBJMa96SuQBZ/UVP9zrHQcf3SxD2O/HjOhVrKDCsX8LaeEaW91ttMPWvo0c6bg==", "a7c0a630-21a3-48d4-a9e7-8f14054ba6b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "IncrementId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec9e1616-afbb-4c81-b09b-6376e9250404", 0, "AQAAAAEAACcQAAAAECf0x4RSzrIk6VImHG9bX5JcAYwelEUPR/SmLv7B9sFHuDIENyAEoNza5HvgyAuajA==", "912484d5-9405-406c-b79c-1c92cab23fd2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "IncrementId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e55cc01-4916-494a-b729-10e428071b05", 0, "AQAAAAEAACcQAAAAEJWWXsrW+213EBn9sLIjtBifw/B8bf4W8cETF/58E/J/qzCwE44ys7KIWVOVTuZn3A==", "15b9d60e-61fd-48b5-b71c-319d4760827c" });
        }
    }
}
