using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RemoveImdbIdFromGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
