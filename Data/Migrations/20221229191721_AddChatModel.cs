using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddChatModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamCodes_TeamId",
                table: "TeamCodes");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCodes_TeamId",
                table: "TeamCodes",
                column: "TeamId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamCodes_TeamId",
                table: "TeamCodes");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCodes_TeamId",
                table: "TeamCodes",
                column: "TeamId");
        }
    }
}
