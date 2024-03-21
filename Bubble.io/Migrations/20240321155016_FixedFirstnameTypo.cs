using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bubble.io.Migrations
{
    /// <inheritdoc />
    public partial class FixedFirstnameTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fistname",
                table: "Profiles",
                newName: "Firstname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Profiles",
                newName: "Fistname");
        }
    }
}
