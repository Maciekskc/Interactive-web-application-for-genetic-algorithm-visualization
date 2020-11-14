using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedReadyToProcreationAndMutationChargeEnabledFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HungryChargeEnabled",
                table: "SetOfMutations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReadyToProcreate",
                table: "LifeParameters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HungryChargeEnabled",
                table: "SetOfMutations");

            migrationBuilder.DropColumn(
                name: "ReadyToProcreate",
                table: "LifeParameters");
        }
    }
}
