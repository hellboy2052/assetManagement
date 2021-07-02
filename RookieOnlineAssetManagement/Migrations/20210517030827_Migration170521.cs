using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class Migration170521 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAssgined",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8",
                column: "ConcurrencyStamp",
                value: "ef1496a9-6b68-45ad-ac64-eee0e1884efd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                column: "ConcurrencyStamp",
                value: "723d062b-a406-44dc-8bb7-47f1c2516290");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "cffbe720-41cf-463f-aec2-532db644b7d1", "AQAAAAEAACcQAAAAECgSxy7+boFPVaqzumzVJZX1p6ox4EtWyEbxfa4wksRehaFJIxDqU/Mb9/u2ZMCZlg==", "64d1b6e3-d524-4f79-9872-7d85a8fabd9b", "Staff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "90a948ed-e449-4e09-ba19-74124c5f7c9d", "AQAAAAEAACcQAAAAEArcHUC0ClHT7cMHPnyec1vGCgbJupcH4WpRzJ1I17NRu8u8FKA5dFy9YiAjgym8UQ==", "1100de72-0de3-4572-9043-c8b572923883", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Type" },
                values: new object[] { "044f0b5b-2411-4b22-893c-f4fefc44476a", "AQAAAAEAACcQAAAAEOTiYOHZSesOSBOVpz1ToylvIrJ9DOgZWX2by5HwA4w53Rv/VQ1TtB6wDl5xcWVyNw==", "2bf1bb04-ee8e-4702-8238-a460997a740b", "Staff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssgined",
                table: "AspNetUsers");

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
