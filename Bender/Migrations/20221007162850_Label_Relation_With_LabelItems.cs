using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bender.Migrations
{
    public partial class Label_Relation_With_LabelItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "LabelItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabelItems_LabelId",
                table: "LabelItems",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelItems_Labels_LabelId",
                table: "LabelItems",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelItems_Labels_LabelId",
                table: "LabelItems");

            migrationBuilder.DropIndex(
                name: "IX_LabelItems_LabelId",
                table: "LabelItems");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "LabelItems");
        }
    }
}
