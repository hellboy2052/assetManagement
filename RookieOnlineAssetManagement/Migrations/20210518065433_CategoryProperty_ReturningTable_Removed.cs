using Microsoft.EntityFrameworkCore.Migrations;

namespace RookieOnlineAssetManagement.Migrations
{
    public partial class CategoryProperty_ReturningTable_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returning_AspNetUsers_AdminId",
                table: "Returning");

            migrationBuilder.DropForeignKey(
                name: "FK_Returning_AspNetUsers_StaffId",
                table: "Returning");

            migrationBuilder.DropForeignKey(
                name: "FK_Returning_Categories_CategoryId",
                table: "Returning");

            migrationBuilder.DropIndex(
                name: "IX_Returning_AdminId",
                table: "Returning");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Returning");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Returning",
                newName: "RequestById");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Returning",
                newName: "AssignedById");

            migrationBuilder.RenameIndex(
                name: "IX_Returning_StaffId",
                table: "Returning",
                newName: "IX_Returning_RequestById");

            migrationBuilder.RenameIndex(
                name: "IX_Returning_CategoryId",
                table: "Returning",
                newName: "IX_Returning_AssignedById");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Returning",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_AspNetUsers_AssignedById",
                table: "Returning",
                column: "AssignedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_AspNetUsers_RequestById",
                table: "Returning",
                column: "RequestById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returning_AspNetUsers_AssignedById",
                table: "Returning");

            migrationBuilder.DropForeignKey(
                name: "FK_Returning_AspNetUsers_RequestById",
                table: "Returning");

            migrationBuilder.RenameColumn(
                name: "RequestById",
                table: "Returning",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "AssignedById",
                table: "Returning",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Returning_RequestById",
                table: "Returning",
                newName: "IX_Returning_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Returning_AssignedById",
                table: "Returning",
                newName: "IX_Returning_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Returning",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Returning",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Returning_AdminId",
                table: "Returning",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_AspNetUsers_AdminId",
                table: "Returning",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_AspNetUsers_StaffId",
                table: "Returning",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Returning_Categories_CategoryId",
                table: "Returning",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
