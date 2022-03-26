using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RemovePluralFromWatchedAndWishListAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedList_Accounts_WatchedListAccountsId",
                table: "WatchedList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Accounts_WishListAccountsId",
                table: "WishList");

            migrationBuilder.RenameColumn(
                name: "WishListAccountsId",
                table: "WishList",
                newName: "WishListAccountId");

            migrationBuilder.RenameColumn(
                name: "WatchedListAccountsId",
                table: "WatchedList",
                newName: "WatchedListAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedList_Accounts_WatchedListAccountId",
                table: "WatchedList",
                column: "WatchedListAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Accounts_WishListAccountId",
                table: "WishList",
                column: "WishListAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedList_Accounts_WatchedListAccountId",
                table: "WatchedList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Accounts_WishListAccountId",
                table: "WishList");

            migrationBuilder.RenameColumn(
                name: "WishListAccountId",
                table: "WishList",
                newName: "WishListAccountsId");

            migrationBuilder.RenameColumn(
                name: "WatchedListAccountId",
                table: "WatchedList",
                newName: "WatchedListAccountsId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedList_Accounts_WatchedListAccountsId",
                table: "WatchedList",
                column: "WatchedListAccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Accounts_WishListAccountsId",
                table: "WishList",
                column: "WishListAccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
