using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class FixAssetReturningFK : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Returning",
                table: "Returning");

            migrationBuilder.DropForeignKey(
               name: "FK_Returning_Assets_ReturnId",
               table: "Returning");

            migrationBuilder.AlterColumn<string>(
                name: "AssetCode",
                table: "Returning",
                type: "char(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);


            migrationBuilder.AlterColumn<string>(
                name: "ReturnId",
                table: "Returning",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(8)");

            migrationBuilder.AddPrimaryKey("PK_Returning", "Returning", "ReturnId");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000000",
                column: "State",
                value: "Available");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000001",
                column: "State",
                value: "Available");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000002",
                column: "State",
                value: "Recycled");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000000",
                column: "State",
                value: "WaitingForRecycling");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000001",
                column: "State",
                value: "NotAvailable");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000002",
                column: "State",
                value: "Assigned");

            migrationBuilder.CreateIndex(
                name: "IX_Returning_AssetCode",
                table: "Returning",
                column: "AssetCode",
                unique: true,
                filter: "[AssetCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_Assets_AssetCode",
                table: "Returning",
                column: "AssetCode",
                principalTable: "Assets",
                principalColumn: "AssestCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returning_Assets_AssetCode",
                table: "Returning");

            migrationBuilder.DropIndex(
                name: "IX_Returning_AssetCode",
                table: "Returning");

            migrationBuilder.AlterColumn<string>(
                name: "AssetCode",
                table: "Returning",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReturnId",
                table: "Returning",
                type: "char(8)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "0f0f2502-db12-4129-b3cd-0d373164dabe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "staff",
                column: "ConcurrencyStamp",
                value: "6f7dd0dc-6ef3-4f32-9d6e-4dc497441324");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminhcm",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8f6e95d-f7ab-434e-923c-5357e9a2bc81", "AQAAAAEAACcQAAAAEP1pQ1u4uKuhp7lYpFX9XudOPV0deN1puExkJHfRRHWONNfFoqm1jjzHZqT0mzj+rw==", "be745857-927d-418a-9b6e-b2d0a833aa72" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminhn",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "091d93c1-3334-4d56-a90a-7f349b6a4eca", "AQAAAAEAACcQAAAAEDsYx5RYsBxql85UStDIcCubxKbBVBmEA9L54OYVw6TYWwTtGadO9/pO+3ddijNI6g==", "6e67667f-79d8-454d-a65e-aa941ee19449" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userhcm",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b93c807-5814-47d8-9b78-fbf941dab87d", "AQAAAAEAACcQAAAAEHWk6inLPxUpYqVG34QqviAmQ8yb55uiEgi64NZ7xBqHzYeMEVVbDwVnLQv1Cv9GkA==", "ede7fc1d-73ec-478b-8c3b-c1df099138e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userhn1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44e52730-37e8-4c74-8997-d01638b0e05d", "AQAAAAEAACcQAAAAEEfdQA7XVfqgO7SZw0U10KVeoV6zjfbWMpN7RHsRjs2MacsFWCNjskad3Cg4bg7P+g==", "6c7335d9-087a-4eb7-8af1-68188c6ec48a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userhn2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4ad66d7-d3d9-424e-ac45-fbf03f3e1e60", "AQAAAAEAACcQAAAAEEO88kDc3Sg1otU3UjHEpQe8c3eQPtpRTxeqk/WYj7NDBWOodEqZ32CeiXpAmz7tVg==", "8f41e93f-101c-4c1b-90d4-c06e2d8a8d12" });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000000",
                column: "State",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000001",
                column: "State",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000002",
                column: "State",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000000",
                column: "State",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000001",
                column: "State",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000002",
                column: "State",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_Assets_ReturnId",
                table: "Returning",
                column: "ReturnId",
                principalTable: "Assets",
                principalColumn: "AssestCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
