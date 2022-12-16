using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEnd",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventStart",
                table: "Events",
                newName: "EventDate");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TeamId",
                table: "Events",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Teams_TeamId",
                table: "Events",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Teams_TeamId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_TeamId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Events",
                newName: "EventStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEnd",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
