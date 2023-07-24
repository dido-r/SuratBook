using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuratBook.Data.Migrations
{
    public partial class AcceptFriendRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SuratUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SuratUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuratUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "AreFriends",
                table: "FriendsRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreFriends",
                table: "FriendsRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "SuratUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SuratUserId",
                table: "AspNetUsers",
                column: "SuratUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SuratUserId",
                table: "AspNetUsers",
                column: "SuratUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
