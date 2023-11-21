using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class ChangeSocialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Socials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Socials",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
