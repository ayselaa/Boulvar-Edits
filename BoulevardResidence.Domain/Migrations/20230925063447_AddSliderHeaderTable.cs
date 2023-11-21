using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddSliderHeaderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SliderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoftDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SliderHeaderTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Tittle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SliderHeaderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderHeaderTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SliderHeaderTranslates_SliderHeaders_SliderHeaderId",
                        column: x => x.SliderHeaderId,
                        principalTable: "SliderHeaders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SliderHeaderTranslates_SliderHeaderId",
                table: "SliderHeaderTranslates",
                column: "SliderHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SliderHeaderTranslates");

            migrationBuilder.DropTable(
                name: "SliderHeaders");
        }
    }
}
