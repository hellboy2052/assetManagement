using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class DefaultValueJoinDate_UserTable_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "date");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73e205ac-e5a8-4c5e-bc88-482b479bd288", "AQAAAAEAACcQAAAAEH3Okf2WkOkNJ7dlntvpB6MMHtklowI23GZQiwPjdas9agAg25k/xYDi3vjnnFrsKQ==", "c67522f8-d71d-42f1-bfa7-63288a4247ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a05454a-caea-4972-8c65-a04f17916334", "AQAAAAEAACcQAAAAEHpLZ9FNij02zkPuTKAtXFejX0RGENwJscBq+s8wFPs7MLtKppAXve/9E+AG0nLkxw==", "2ea046f0-cf25-4bc7-9464-8a0a0fe705fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b37273e0-1453-414e-b054-454c73c5f7cf", "AQAAAAEAACcQAAAAEICD0iMPkkyiaWJtLGc78/V6m3c7YuvPi1THP/yFUrh4YEi0WuA4z2dduVH9HT79Gw==", "50209fcb-20ce-4146-8f2d-da477caca0ee" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true,
                filter: "[CategoryName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "e3f5b504-d69c-4c2f-8fa2-d560644ec8ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "108ed0ac-1b9c-4364-8c40-cb93b9698182");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc038ebf-7211-4ea8-8f06-a53501f88c69", "AQAAAAEAACcQAAAAEB/Gf0oI3ZpKIStn/A56Wod744e+T2ErKePRtS7aU4fddjvlwwbWOUJzYMA298X2Bw==", "88094f3d-c6cf-41c1-8fcf-0e6b2a49b2e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ea003b7-b59d-4a5f-9f90-6ee9c081e2a2", "AQAAAAEAACcQAAAAEGWtwTRzCD0PrHTdCvRkHsgZJoS/75vJbsLk7CbSAk7VeHDl7JGNd75DxS9JdlMf7Q==", "787e1d32-ed85-47db-8892-783e5d232055" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "505044a5-856c-4fea-a1b5-0fc6f95297f3", "AQAAAAEAACcQAAAAEBhSVx04vvIIMWV+afMAQZtFVtefXAJwacIpx3RETReS9NyHrXB9u/AUU7eTTKeORA==", "1a0cd2a5-f571-4443-9c31-426710cb1e66" });
        }
    }
}
