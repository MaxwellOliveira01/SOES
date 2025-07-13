using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOE.Migrations
{
    /// <inheritdoc />
    public partial class Server : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoterPublicKeyPem",
                table: "VoterElections");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Signature",
                table: "VoterElections",
                type: "BLOB",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ServerId",
                table: "VoterElections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ServerSignature",
                table: "VoterElections",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "VoterPublicKey",
                table: "VoterElections",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartsUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublicKey = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoterElections_ServerId",
                table: "VoterElections",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterElections_Servers_ServerId",
                table: "VoterElections",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterElections_Servers_ServerId",
                table: "VoterElections");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_VoterElections_ServerId",
                table: "VoterElections");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "VoterElections");

            migrationBuilder.DropColumn(
                name: "ServerSignature",
                table: "VoterElections");

            migrationBuilder.DropColumn(
                name: "VoterPublicKey",
                table: "VoterElections");

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "VoterElections",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AddColumn<string>(
                name: "VoterPublicKeyPem",
                table: "VoterElections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
