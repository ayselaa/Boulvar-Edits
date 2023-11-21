using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class ChangesOnBackgroundImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "SectionBackgroundImages",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SectionBackgroundImages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "SectionBackgroundImages");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SectionBackgroundImages",
                newName: "Image");
        }
    }
}
