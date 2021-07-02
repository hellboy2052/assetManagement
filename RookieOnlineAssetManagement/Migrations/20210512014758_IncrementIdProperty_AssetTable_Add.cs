using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class IncrementIdProperty_AssetTable_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncrementId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

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
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc038ebf-7211-4ea8-8f06-a53501f88c69", "Binh", "Nguyen Van", "AQAAAAEAACcQAAAAEB/Gf0oI3ZpKIStn/A56Wod744e+T2ErKePRtS7aU4fddjvlwwbWOUJzYMA298X2Bw==", "88094f3d-c6cf-41c1-8fcf-0e6b2a49b2e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ea003b7-b59d-4a5f-9f90-6ee9c081e2a2", "An", "Nguyen Thuy", "AQAAAAEAACcQAAAAEGWtwTRzCD0PrHTdCvRkHsgZJoS/75vJbsLk7CbSAk7VeHDl7JGNd75DxS9JdlMf7Q==", "787e1d32-ed85-47db-8892-783e5d232055" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "505044a5-856c-4fea-a1b5-0fc6f95297f3", "An", "Tran Van", "AQAAAAEAACcQAAAAEBhSVx04vvIIMWV+afMAQZtFVtefXAJwacIpx3RETReS9NyHrXB9u/AUU7eTTKeORA==", "1a0cd2a5-f571-4443-9c31-426710cb1e66" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncrementId",
                table: "Assets");

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
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ca58332-b8ea-4e5a-acc8-578c3ed4276e", "Nguyen Van", "Binh", "AQAAAAEAACcQAAAAELQBDpkNK+8vgvLYuHSFysNDWCM3dHmixdNEMZ+QW1RdkxYdr/fCFrXp7zrpBbYsZQ==", "0d79f818-6640-42ea-bc37-385514ba3e6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad95f059-7f2b-4b3a-851c-d8cedba50191", "Nguyen Thuy", "An", "AQAAAAEAACcQAAAAEHP+vEnceS/XMTlV6ezF/4sjhTt33XsTZkJgmx34iSL7pqn9bAeGnjHOZ5v5trrDkg==", "1ba9d3cf-89ed-4f30-9350-982ca393f090" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d246c6e-5921-467d-b81e-226189179d46", "Tran Van", "An", "AQAAAAEAACcQAAAAEAZbCkzucBMji1BsiyFBqp4UQ8aojP6Lagx3jtYYIKYTMxRf1TVV6Uvc1e0/HOXxBA==", "23e73f04-8c96-458a-bdf6-4d32cc562063" });
        }
    }
}
