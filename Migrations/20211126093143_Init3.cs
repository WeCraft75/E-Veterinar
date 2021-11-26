using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Veterinar.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TERMIN_AspNetUsers_UserId",
                table: "TERMIN");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TERMIN",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_TERMIN_UserId",
                table: "TERMIN",
                newName: "IX_TERMIN_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TERMIN_AspNetUsers_OwnerId",
                table: "TERMIN",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TERMIN_AspNetUsers_OwnerId",
                table: "TERMIN");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TERMIN",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TERMIN_OwnerId",
                table: "TERMIN",
                newName: "IX_TERMIN_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TERMIN_AspNetUsers_UserId",
                table: "TERMIN",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
