using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TayDuKy.Migrations
{
    public partial class updatedatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpadtedDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpadtedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "User");
        }
    }
}
