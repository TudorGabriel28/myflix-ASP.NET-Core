using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class FixMovieRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Movies_MovieId",
                table: "Genres");

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

            migrationBuilder.DropIndex(
                name: "IX_Genres_MovieId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterRankings",
                table: "MeterRankings");

            migrationBuilder.DropColumn(
                name: "EpisodesId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MeterRankingId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PrimaryImageId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "MeterRankings",
                newName: "MeterRanking");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "PrimaryImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "MeterRanking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterRanking",
                table: "MeterRanking",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryImages_MovieId",
                table: "PrimaryImages",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_MovieId",
                table: "Episodes",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeterRanking_MovieId",
                table: "MeterRanking",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Movies_MovieId",
                table: "Episodes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeterRanking_Movies_MovieId",
                table: "MeterRanking",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Movies_MovieId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeterRanking_Movies_MovieId",
                table: "MeterRanking");

            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryImages_Movies_MovieId",
                table: "PrimaryImages");

            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryImages_MovieId",
                table: "PrimaryImages");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_MovieId",
                table: "Episodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterRanking",
                table: "MeterRanking");

            migrationBuilder.DropIndex(
                name: "IX_MeterRanking_MovieId",
                table: "MeterRanking");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "PrimaryImages");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "MeterRanking");

            migrationBuilder.RenameTable(
                name: "MeterRanking",
                newName: "MeterRankings");

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

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterRankings",
                table: "MeterRankings",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieId",
                table: "Genres",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Movies_MovieId",
                table: "Genres",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

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
    }
}
