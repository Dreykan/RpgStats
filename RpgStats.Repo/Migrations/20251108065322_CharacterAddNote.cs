using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgStats.Repo.Migrations
{
    /// <inheritdoc />
    public partial class CharacterAddNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Characters",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Characters");
        }
    }
}
