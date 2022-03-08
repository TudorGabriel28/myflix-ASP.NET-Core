using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RemoveFKFromEpisodesImageMeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Movies_MovieId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeterRankings_Movies_MovieId",
                table: "MeterRankings");

            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryImages_Movies_MovieId",
                table: "PrimaryImages");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryImages_MovieId",
                table: "PrimaryImages");

            migrationBuilder.DropIndex(
                name: "IX_MeterRankings_MovieId",
                table: "MeterRankings");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_MovieId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "PrimaryImages");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "MeterRankings");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Episodes");

            migrationBuilder.AddColumn<int>(
                name: "EpisodesId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeterRankingId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryImageId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_EpisodesId",
                table: "Movies",
                column: "EpisodesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MeterRankingId",
                table: "Movies",
                column: "MeterRankingId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PrimaryImageId",
                table: "Movies",
                column: "PrimaryImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Episodes_EpisodesId",
                table: "Movies",
                column: "EpisodesId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MeterRankings_MeterRankingId",
                table: "Movies",
                column: "MeterRankingId",
                principalTable: "MeterRankings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_PrimaryImages_PrimaryImageId",
                table: "Movies",
                column: "PrimaryImageId",
                principalTable: "PrimaryImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Episodes_EpisodesId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MeterRankings_MeterRankingId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_PrimaryImages_PrimaryImageId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_EpisodesId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MeterRankingId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_PrimaryImageId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "EpisodesId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MeterRankingId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PrimaryImageId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "PrimaryImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "MeterRankings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryImages_MovieId",
                table: "PrimaryImages",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeterRankings_MovieId",
                table: "MeterRankings",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_MovieId",
                table: "Episodes",
                column: "MovieId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Movies_MovieId",
                table: "Episodes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeterRankings_Movies_MovieId",
                table: "MeterRankings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryImages_Movies_MovieId",
                table: "PrimaryImages",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
