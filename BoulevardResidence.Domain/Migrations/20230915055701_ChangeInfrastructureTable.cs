using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class ChangeInfrastructureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Infrastructures");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InfrastructureTranslates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Infrastructures",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InfrastructureTranslates");

            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Infrastructures");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Infrastructures",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
