using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ModifyInversePropertyAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchedMovies",
                columns: table => new
                {
                    WatchedMoviesAccountsId = table.Column<int>(type: "int", nullable: false),
                    WatchedMoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedMovies", x => new { x.WatchedMoviesAccountsId, x.WatchedMoviesId });
                    table.ForeignKey(
                        name: "FK_WatchedMovies_Accounts_WatchedMoviesAccountsId",
                        column: x => x.WatchedMoviesAccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchedMovies_Movies_WatchedMoviesId",
                        column: x => x.WatchedMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    WishListAccountsId = table.Column<int>(type: "int", nullable: false),
                    WishListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => new { x.WishListAccountsId, x.WishListId });
                    table.ForeignKey(
                        name: "FK_WishList_Accounts_WishListAccountsId",
                        column: x => x.WishListAccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishList_Movies_WishListId",
                        column: x => x.WishListId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_WatchedMoviesId",
                table: "WatchedMovies",
                column: "WatchedMoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_WishListId",
                table: "WishList",
                column: "WishListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedMovies");

            migrationBuilder.DropTable(
                name: "WishList");
        }
    }
}
