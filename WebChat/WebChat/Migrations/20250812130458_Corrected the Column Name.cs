using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebChat.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedtheColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_User_RecieverId",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "RecieverId",
                table: "ChatMessages",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_RecieverId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_User_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_User_ReceiverId",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "ChatMessages",
                newName: "RecieverId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_RecieverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_User_RecieverId",
                table: "ChatMessages",
                column: "RecieverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
