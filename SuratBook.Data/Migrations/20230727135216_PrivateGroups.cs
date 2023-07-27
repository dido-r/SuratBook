using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuratBook.Data.Migrations
{
    public partial class PrivateGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupsJoinRequestFeedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsJoinRequestFeedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupsJoinRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuratUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrouptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsJoinRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsJoinRequests_AspNetUsers_SuratUserId",
                        column: x => x.SuratUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsJoinRequests_Groups_GrouptId",
                        column: x => x.GrouptId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsJoinRequests_GrouptId",
                table: "GroupsJoinRequests",
                column: "GrouptId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsJoinRequests_SuratUserId",
                table: "GroupsJoinRequests",
                column: "SuratUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsJoinRequestFeedbacks");

            migrationBuilder.DropTable(
                name: "GroupsJoinRequests");
        }
    }
}
