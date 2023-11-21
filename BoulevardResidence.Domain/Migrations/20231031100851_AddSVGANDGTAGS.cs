using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddSVGANDGTAGS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GTagPlan",
                table: "Apartments",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FloorPlan",
                table: "ApartmentFloors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "SVGPlan",
                table: "ApartmentFloors",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GTagPlan",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "SVGPlan",
                table: "ApartmentFloors");

            migrationBuilder.AlterColumn<string>(
                name: "FloorPlan",
                table: "ApartmentFloors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
