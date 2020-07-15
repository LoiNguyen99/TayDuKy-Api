using Microsoft.EntityFrameworkCore.Migrations;

namespace TayDuKy.Migrations
{
    public partial class removeEquipmentInCamity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Calamity_CalamityId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_CalamityId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CalamityId",
                table: "Equipment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalamityId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CalamityId",
                table: "Equipment",
                column: "CalamityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Calamity_CalamityId",
                table: "Equipment",
                column: "CalamityId",
                principalTable: "Calamity",
                principalColumn: "CalamityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
