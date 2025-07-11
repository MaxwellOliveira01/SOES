using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOE.Migrations
{
    /// <inheritdoc />
    public partial class RenameTokenToOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Voters_VoterId",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "Otps");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_VoterId",
                table: "Otps",
                newName: "IX_Otps_VoterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Otps",
                table: "Otps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Otps_Voters_VoterId",
                table: "Otps",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otps_Voters_VoterId",
                table: "Otps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Otps",
                table: "Otps");

            migrationBuilder.RenameTable(
                name: "Otps",
                newName: "Tokens");

            migrationBuilder.RenameIndex(
                name: "IX_Otps_VoterId",
                table: "Tokens",
                newName: "IX_Tokens_VoterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Voters_VoterId",
                table: "Tokens",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
