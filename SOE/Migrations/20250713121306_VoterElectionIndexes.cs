using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOE.Migrations
{
    /// <inheritdoc />
    public partial class VoterElectionIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoterElections_ServerSignature",
                table: "VoterElections",
                column: "ServerSignature",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoterElections_Signature",
                table: "VoterElections",
                column: "Signature",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoterElections_ServerSignature",
                table: "VoterElections");

            migrationBuilder.DropIndex(
                name: "IX_VoterElections_Signature",
                table: "VoterElections");
        }
    }
}
