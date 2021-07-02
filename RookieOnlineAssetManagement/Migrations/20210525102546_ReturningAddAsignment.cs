using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class ReturningAddAsignment : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedDate",
                table: "Returning");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentId",
                table: "Returning",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
             name: "IX_Returning_AssignmentId",
             table: "Returning",
             column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_Assignments_AssignmentId",
                table: "Returning",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returning_Assignments_AssignmentId",
                table: "Returning");

            migrationBuilder.DropIndex(
                name: "IX_Returning_AssignmentId",
                table: "Returning");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Returning");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedDate",
                table: "Returning",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
