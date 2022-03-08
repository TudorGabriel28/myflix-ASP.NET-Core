using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ChangeMovieUploadedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Accounts_UploadedById",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UploadedById",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movies_UploadedById",
                table: "Movies",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Accounts_UploadedById",
                table: "Movies",
                column: "UploadedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
