using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39", "308a0f9c-2744-45d1-96c6-3c2f624f284c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c581eb54-d110-4177-b851-7a616d52abc8", "5f6f60e1-433b-4c02-8af2-46fb29be8568" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39", "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c581eb54-d110-4177-b851-7a616d52abc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin", "f7b83b8d-84fc-41fb-ac39-1d44ff136331", "Admin", "ADMIN" },
                    { "staff", "3cc349b3-0388-451a-827d-111bf017ce37", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "IsAssigned", "IsDefaultPassword", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[,]
                {
                    { "adminhn", 0, "4c3b74ee-582e-486b-8ebf-bb19a2f9a8b0", new DateTime(1999, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "An", true, 1, false, true, new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Thuy", "HN", false, null, null, "ADMINHN", "AQAAAAEAACcQAAAAEOdu6YGKHZtFYcu/WZpae8XwD6M9760wqJIUkcGK3CWur5YYwRqJoWsHV3UpdhRAvQ==", null, false, "f102513f-cdd2-485b-87b9-83ee5509cf89", false, "Admin", "adminhn" },
                    { "adminhcm", 0, "52ff85b8-2cd1-46a9-a7da-82d676e0c8a4", new DateTime(1993, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "An", false, 2, false, true, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tran Van", "HCM", false, null, null, "ADMINHCM", "AQAAAAEAACcQAAAAELdsVf9Bc/4NFvisjh7BFHX2QxePdpxAqMQiuZec6br4oLwsYxcm9b5gn64R3kTk8w==", null, false, "a7e90880-7b29-4f0b-bfdb-bf89a12887fe", false, "Admin", "adminhcm" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "IsAssigned", "IsDefaultPassword", "IsDisabled", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "userhn1", 0, "2b2f34ad-5ca4-41fc-8aca-463496efcc3c", new DateTime(1996, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Toai", false, 3, false, true, true, new DateTime(2018, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Van", "HN", false, null, null, "BINHNT", "AQAAAAEAACcQAAAAEM7lagqeT28C0qHLEG8elz5lbbcpMpYCTv4DlwajH3qYVtXD+3ffd7LsFRcJ1hNHLw==", null, false, "e5e28bda-a9e7-4706-9b58-29127c7cb124", false, "Staff", "binhnt" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "IsAssigned", "IsDefaultPassword", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[,]
                {
                    { "userhcm", 0, "fccca1d9-cf60-4d1c-83b5-37380a551f0e", new DateTime(1996, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Binh", true, 4, false, true, new DateTime(2018, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Van", "HCM", false, null, null, "BINHNV", "AQAAAAEAACcQAAAAEBY4qK5Cd2jpjSG1VjhFCJ0y/5rXlj7gMZfqggNDu6h4RkdVo/G++64hynFTSxxvpw==", null, false, "63354150-df78-4438-a7dd-ef404abc2a32", false, "Staff", "binhnv" },
                    { "userhn2", 0, "fe536dfd-d260-4603-8d9a-ca595e956309", new DateTime(1996, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Binh", true, 5, false, true, new DateTime(2018, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vo Nguyen", "HN", false, null, null, "BINHNV1", "AQAAAAEAACcQAAAAEDBiklt/zis6lC/nbIsH7g3mbW+o59LSUgQzyKFdTUodyQn1Xsh8dUNfG+MlZvGRow==", null, false, "c89488d5-7add-4998-afe4-28646a83cd9c", false, "Staff", "binhnv1" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { "LA", "Laptop" },
                    { "PC", "Personal Computer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Location", "HN", "adminhn" },
                    { 2, "Location", "HCM", "adminhcm" },
                    { 3, "Location", "HN", "userhn1" },
                    { 4, "Location", "HCM", "userhcm" },
                    { 5, "Location", "HN", "userhn2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "admin", "adminhn" },
                    { "admin", "adminhcm" },
                    { "staff", "userhn1" },
                    { "staff", "userhcm" },
                    { "staff", "userhn2" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssestCode", "AssestName", "CategoryId", "InstallDate", "IsAssigned", "Location", "Specification", "State" },
                values: new object[,]
                {
                    { "LA000000", "Lenovo Thinkpad T440p", "LA", new DateTime(2014, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HCM", "This is a simple Specification from HCM", null },
                    { "LA000001", "Lenovo Thinkpad X240", "LA", new DateTime(2014, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HN", "This is a simple Specification from HN", null },
                    { "LA000002", "Lenovo Thinkpad T440p", "LA", new DateTime(2016, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HN", "This is a simple Specification from HN", null },
                    { "PC000000", "PC GTX 2060", "PC", new DateTime(2020, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HCM", "This is a simple Specification from HCM", null },
                    { "PC000001", "PC GTX 1060", "PC", new DateTime(2018, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HCM", "This is a simple Specification from HCM", null },
                    { "PC000002", "PC GTX 660", "PC", new DateTime(2010, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HN", "This is a simple Specification from HN", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "admin", "adminhcm" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "admin", "adminhn" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "staff", "userhcm" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "staff", "userhn1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "staff", "userhn2" });

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000000");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000001");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "LA000002");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000000");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000001");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "AssestCode",
                keyValue: "PC000002");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "staff");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminhcm");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminhn");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userhcm");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userhn1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userhn2");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "LA");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "PC");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c581eb54-d110-4177-b851-7a616d52abc8", "828edb95-5076-4711-992e-c9c0c3a66831", "Admin", "ADMIN" },
                    { "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39", "9ef656b9-3094-47e2-b400-8faf6c43cf09", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "IsAssigned", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[,]
                {
                    { "5f6f60e1-433b-4c02-8af2-46fb29be8568", 0, "e5317970-2178-4f77-836d-0ed8d2cdd94f", new DateTime(1999, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "An", true, 1, false, new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Thuy", "HCM", false, null, null, null, "AQAAAAEAACcQAAAAEAgo3jY61dmFAbBfq1vS/Vzfq/EJQGv7uyoEVfaFBNWzqjxkOtOnelb6Orrf2ZIoXw==", null, false, "237f1938-55ab-4965-9619-ff6e5cdeebf9", false, "Staff", "annt" },
                    { "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a", 0, "ea7929d6-4318-49ba-bb6c-d4fde34d1650", new DateTime(1993, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "An", false, 2, false, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tran Van", "HCM", false, null, null, null, "AQAAAAEAACcQAAAAEJBU4mpgHKWiFBA6DHc/H8Wme7+r5ZeIFxD2XoIvlQd5YvFanQ7CAYRbg6uAg8XLFw==", null, false, "7e39b3e5-2ef1-417f-805a-57bb60fc4a1c", false, "Admin", "antv" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "Gender", "IncrementId", "IsAssigned", "IsDisabled", "JoinedDate", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "308a0f9c-2744-45d1-96c6-3c2f624f284c", 0, "5f172fea-2b55-4427-8895-d2f86aac168a", new DateTime(1996, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Binh", true, 3, false, true, new DateTime(2018, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Van", "HN", false, null, null, null, "AQAAAAEAACcQAAAAEJY1gNE9KgKUJB67lrmgIfSlzhxeX+D3//FD2ne4E++m938QiUv9R3PFlxkxD2TlzA==", null, false, "105211be-e332-4c5a-834c-14a52bebdd25", false, "Admin", "binhnv1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c581eb54-d110-4177-b851-7a616d52abc8", "5f6f60e1-433b-4c02-8af2-46fb29be8568" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39", "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39", "308a0f9c-2744-45d1-96c6-3c2f624f284c" });
        }
    }
}
