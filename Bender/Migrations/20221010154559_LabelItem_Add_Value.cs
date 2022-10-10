using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bender.Migrations
{
    public partial class LabelItem_Add_Value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "LabelItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "LabelItems");
        }
    }
}
