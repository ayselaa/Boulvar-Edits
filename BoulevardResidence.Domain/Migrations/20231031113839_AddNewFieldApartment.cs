using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddNewFieldApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotAviableGTagPlan",
                table: "Apartments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotAviableGTagPlan",
                table: "Apartments");
        }
    }
}
