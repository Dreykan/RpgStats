using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgStats.Repo.Migrations
{
    public partial class newColumnsInTableStatValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainedBonusNum",
                table: "StatValues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContainedBonusPercent",
                table: "StatValues",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainedBonusNum",
                table: "StatValues");

            migrationBuilder.DropColumn(
                name: "ContainedBonusPercent",
                table: "StatValues");
        }
    }
}
