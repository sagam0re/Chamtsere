using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chamtsere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTokenColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "Tokens",
                newName: "RefreshToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "Tokens",
                newName: "AccessToken");
        }
    }
}
