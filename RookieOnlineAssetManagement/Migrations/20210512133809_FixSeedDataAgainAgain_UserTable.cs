using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class FixSeedDataAgainAgain_UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "c4c55a8b-2b92-4f47-a6cc-96693d696e54");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "d14f4318-130e-455e-828c-2bbf090e2f59");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "76bdab72-091e-4c80-8d81-c006262ec672", "AQAAAAEAACcQAAAAEBxpDzMAyjESA48P9Ka8pd7QkxqCvGyeW5v3XgzdOz4ToMYTOpdbczT33V5lFelL6w==", "7f006851-97d3-4059-8b4d-506ea7e3b6c5", "Staff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "0f19c30c-6bd9-4461-a527-fc6c0b7619f8", "AQAAAAEAACcQAAAAEG+SEICBf1/cRvOL8MvWEG7HfcDXf1Nh3/WWr4zrvpmnQk72StytxCLzqFpygkmfOA==", "100d1499-5a62-4e19-8185-981144c92c9f", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "0832bc0f-2e8e-49ca-acc5-b7c293df7821", "AQAAAAEAACcQAAAAEOLu50VaZd8Azjsnwv9FHBlT5XzgCiZuCJpcrewjx3edTDJ5Ja8ZHsoO8qSWnCEh8A==", "316d8753-28ee-4c91-82c4-50eec058f439", "Staff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "36792535-1fa5-4439-a201-aba36b39ae77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "e60e4f20-0343-45d9-9efe-9b8f09994c4b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "73e205ac-e5a8-4c5e-bc88-482b479bd288", "AQAAAAEAACcQAAAAEH3Okf2WkOkNJ7dlntvpB6MMHtklowI23GZQiwPjdas9agAg25k/xYDi3vjnnFrsKQ==", "c67522f8-d71d-42f1-bfa7-63288a4247ba", "Staff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "0a05454a-caea-4972-8c65-a04f17916334", "AQAAAAEAACcQAAAAEHpLZ9FNij02zkPuTKAtXFejX0RGENwJscBq+s8wFPs7MLtKppAXve/9E+AG0nLkxw==", "2ea046f0-cf25-4bc7-9464-8a0a0fe705fc", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "b37273e0-1453-414e-b054-454c73c5f7cf", "AQAAAAEAACcQAAAAEICD0iMPkkyiaWJtLGc78/V6m3c7YuvPi1THP/yFUrh4YEi0WuA4z2dduVH9HT79Gw==", "50209fcb-20ce-4146-8f2d-da477caca0ee", "Staff" });
        }
    }
}
