using Microsoft.EntityFrameworkCore.Migrations;

namespace TayDuKy.Migrations
{
    public partial class _2ndCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalamityEquipment",
                columns: table => new
                {
                    CalamityId = table.Column<int>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalamityEquipment", x => new { x.CalamityId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_CalamityEquipment_Calamity_CalamityId",
                        column: x => x.CalamityId,
                        principalTable: "Calamity",
                        principalColumn: "CalamityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalamityEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalamityEquipment_EquipmentId",
                table: "CalamityEquipment",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalamityEquipment");
        }
    }
}
