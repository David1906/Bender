using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bender.Migrations
{
    public partial class LabelItem_Delete_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelItems_Labels_LabelId",
                table: "LabelItems");

            migrationBuilder.AlterColumn<int>(
                name: "LabelId",
                table: "LabelItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelItems_Labels_LabelId",
                table: "LabelItems",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelItems_Labels_LabelId",
                table: "LabelItems");

            migrationBuilder.AlterColumn<int>(
                name: "LabelId",
                table: "LabelItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelItems_Labels_LabelId",
                table: "LabelItems",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId");
        }
    }
}
