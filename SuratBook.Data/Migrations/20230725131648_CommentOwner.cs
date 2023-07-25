using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuratBook.Data.Migrations
{
    public partial class CommentOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_OwmerId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "OwmerId",
                table: "Comments",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_OwmerId",
                table: "Comments",
                newName: "IX_Comments_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerId",
                table: "Comments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Comments",
                newName: "OwmerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments",
                newName: "IX_Comments_OwmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_OwmerId",
                table: "Comments",
                column: "OwmerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
