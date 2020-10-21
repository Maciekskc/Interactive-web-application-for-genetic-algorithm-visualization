using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedAssociationsOnUserAndFish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Fishes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fishes_OwnerId",
                table: "Fishes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fishes_AspNetUsers_OwnerId",
                table: "Fishes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fishes_AspNetUsers_OwnerId",
                table: "Fishes");

            migrationBuilder.DropIndex(
                name: "IX_Fishes_OwnerId",
                table: "Fishes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Fishes");
        }
    }
}
