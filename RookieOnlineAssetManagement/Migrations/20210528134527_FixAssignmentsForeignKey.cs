using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class FixAssignmentsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssetCode",
                table: "Assignments");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefaultPassword",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssetCode",
                table: "Assignments",
                column: "AssetCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssetCode",
                table: "Assignments");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssetCode",
                table: "Assignments",
                column: "AssetCode",
                unique: true);
        }
    }
}
