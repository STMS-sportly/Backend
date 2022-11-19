using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamCodeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserTeamEmail",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserTeamTeamId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamCodeCode",
                table: "Teams",
                type: "nvarchar(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamCodes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCodes", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTeamTeamId_UserTeamEmail",
                table: "Users",
                columns: new[] { "UserTeamTeamId", "UserTeamEmail" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamCodeCode",
                table: "Teams",
                column: "TeamCodeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamCodes_TeamCodeCode",
                table: "Teams",
                column: "TeamCodeCode",
                principalTable: "TeamCodes",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersTeams_UserTeamTeamId_UserTeamEmail",
                table: "Users",
                columns: new[] { "UserTeamTeamId", "UserTeamEmail" },
                principalTable: "UsersTeams",
                principalColumns: new[] { "TeamId", "Email" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamCodes_TeamCodeCode",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersTeams_UserTeamTeamId_UserTeamEmail",
                table: "Users");

            migrationBuilder.DropTable(
                name: "TeamCodes");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTeamTeamId_UserTeamEmail",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamCodeCode",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UserTeamEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTeamTeamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamCodeCode",
                table: "Teams");
        }
    }
}
