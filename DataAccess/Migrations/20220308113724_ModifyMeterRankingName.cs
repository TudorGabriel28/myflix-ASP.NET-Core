using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ModifyMeterRankingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterRanking_Movies_MovieId",
                table: "MeterRanking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterRanking",
                table: "MeterRanking");

            migrationBuilder.RenameTable(
                name: "MeterRanking",
                newName: "MeterRankings");

            migrationBuilder.RenameIndex(
                name: "IX_MeterRanking_MovieId",
                table: "MeterRankings",
                newName: "IX_MeterRankings_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterRankings",
                table: "MeterRankings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterRankings_Movies_MovieId",
                table: "MeterRankings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterRankings_Movies_MovieId",
                table: "MeterRankings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterRankings",
                table: "MeterRankings");

            migrationBuilder.RenameTable(
                name: "MeterRankings",
                newName: "MeterRanking");

            migrationBuilder.RenameIndex(
                name: "IX_MeterRankings_MovieId",
                table: "MeterRanking",
                newName: "IX_MeterRanking_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterRanking",
                table: "MeterRanking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterRanking_Movies_MovieId",
                table: "MeterRanking",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
