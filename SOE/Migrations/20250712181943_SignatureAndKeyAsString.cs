using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOE.Migrations
{
    /// <inheritdoc />
    public partial class SignatureAndKeyAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoterPublicKey",
                table: "VoterElections");

            migrationBuilder.DropColumn(
                name: "VoterSignature",
                table: "VoterElections");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "VoterElections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VoterPublicKeyPem",
                table: "VoterElections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "VoterElections");

            migrationBuilder.DropColumn(
                name: "VoterPublicKeyPem",
                table: "VoterElections");

            migrationBuilder.AddColumn<byte[]>(
                name: "VoterPublicKey",
                table: "VoterElections",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "VoterSignature",
                table: "VoterElections",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
