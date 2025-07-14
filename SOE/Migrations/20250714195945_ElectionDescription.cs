using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOE.Migrations
{
    /// <inheritdoc />
    public partial class ElectionDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoterElections_Signature",
                table: "VoterElections");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Elections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Elections");

            migrationBuilder.CreateIndex(
                name: "IX_VoterElections_Signature",
                table: "VoterElections",
                column: "Signature",
                unique: true);
        }
    }
}
