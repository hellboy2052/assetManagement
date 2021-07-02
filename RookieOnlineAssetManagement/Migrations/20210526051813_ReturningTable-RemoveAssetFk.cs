using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class ReturningTableRemoveAssetFk : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returning_Assets_AssetCode",
                table: "Returning");

            migrationBuilder.DropIndex(
                name: "IX_Returning_AssetCode",
                table: "Returning");

            migrationBuilder.DropColumn(
                name: "AssetCode",
                table: "Returning");
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetCode",
                table: "Returning",
                type: "char(8)",
                nullable: true);

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
    }
}
