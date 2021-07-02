using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class isAssignedProperty_AssetTable_Add : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Assets",
                newName: "IsAssigned");
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAssigned",
                table: "Assets",
                newName: "IsDisabled");
        }
    }
}
