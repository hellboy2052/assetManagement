using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class RemoveDuplicatedIsAssigned_UserTable : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsAssgined", table: "AspNetUsers");
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
        }
    }
}
