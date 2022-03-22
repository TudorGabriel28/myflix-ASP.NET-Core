using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RenameWatchedMoviesToWatchList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_Accounts_WatchedMoviesAccountsId",
                table: "WatchedMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_Movies_WatchedMoviesId",
                table: "WatchedMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchedMovies",
                table: "WatchedMovies");

            migrationBuilder.RenameTable(
                name: "WatchedMovies",
                newName: "WatchedList");

            migrationBuilder.RenameColumn(
                name: "WatchedMoviesId",
                table: "WatchedList",
                newName: "WatchedListId");

            migrationBuilder.RenameColumn(
                name: "WatchedMoviesAccountsId",
                table: "WatchedList",
                newName: "WatchedListAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchedMovies_WatchedMoviesId",
                table: "WatchedList",
                newName: "IX_WatchedList_WatchedListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchedList",
                table: "WatchedList",
                columns: new[] { "WatchedListAccountsId", "WatchedListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedList_Accounts_WatchedListAccountsId",
                table: "WatchedList",
                column: "WatchedListAccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedList_Movies_WatchedListId",
                table: "WatchedList",
                column: "WatchedListId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedList_Accounts_WatchedListAccountsId",
                table: "WatchedList");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedList_Movies_WatchedListId",
                table: "WatchedList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchedList",
                table: "WatchedList");

            migrationBuilder.RenameTable(
                name: "WatchedList",
                newName: "WatchedMovies");

            migrationBuilder.RenameColumn(
                name: "WatchedListId",
                table: "WatchedMovies",
                newName: "WatchedMoviesId");

            migrationBuilder.RenameColumn(
                name: "WatchedListAccountsId",
                table: "WatchedMovies",
                newName: "WatchedMoviesAccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchedList_WatchedListId",
                table: "WatchedMovies",
                newName: "IX_WatchedMovies_WatchedMoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchedMovies",
                table: "WatchedMovies",
                columns: new[] { "WatchedMoviesAccountsId", "WatchedMoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_Accounts_WatchedMoviesAccountsId",
                table: "WatchedMovies",
                column: "WatchedMoviesAccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_Movies_WatchedMoviesId",
                table: "WatchedMovies",
                column: "WatchedMoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
