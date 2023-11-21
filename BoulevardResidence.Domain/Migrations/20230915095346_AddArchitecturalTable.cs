using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddArchitecturalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Architecturals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoftDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Architecturals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchitecturalTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: false),
                    ArchitecturalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchitecturalTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchitecturalTranslates_Architecturals_ArchitecturalId",
                        column: x => x.ArchitecturalId,
                        principalTable: "Architecturals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchitecturalTranslates_ArchitecturalId",
                table: "ArchitecturalTranslates",
                column: "ArchitecturalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchitecturalTranslates");

            migrationBuilder.DropTable(
                name: "Architecturals");
        }
    }
}
