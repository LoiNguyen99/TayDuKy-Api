using Microsoft.EntityFrameworkCore.Migrations;

namespace TayDuKy.Migrations
{
    public partial class addIsDeleteCalamity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Calamity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Calamity");
        }
    }
}
