using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class FixParentChildAsotiations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentChild_Fishes_ChildId",
                table: "ParentChild");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentChild_Fishes_ChildId1",
                table: "ParentChild");

            migrationBuilder.DropIndex(
                name: "IX_ParentChild_ChildId1",
                table: "ParentChild");

            migrationBuilder.DropColumn(
                name: "ChildId1",
                table: "ParentChild");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParentChild");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentChild_Fishes_ChildId",
                table: "ParentChild",
                column: "ChildId",
                principalTable: "Fishes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentChild_Fishes_ParentId",
                table: "ParentChild",
                column: "ParentId",
                principalTable: "Fishes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentChild_Fishes_ChildId",
                table: "ParentChild");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentChild_Fishes_ParentId",
                table: "ParentChild");

            migrationBuilder.AddColumn<int>(
                name: "ChildId1",
                table: "ParentChild",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ParentChild",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ParentChild_ChildId1",
                table: "ParentChild",
                column: "ChildId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentChild_Fishes_ChildId",
                table: "ParentChild",
                column: "ChildId",
                principalTable: "Fishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentChild_Fishes_ChildId1",
                table: "ParentChild",
                column: "ChildId1",
                principalTable: "Fishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
