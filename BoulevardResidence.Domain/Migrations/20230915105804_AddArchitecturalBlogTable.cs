using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddArchitecturalBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchitecturalBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchitecturalBlogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchitecturalBlogTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ArchitecturalBlogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchitecturalBlogTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchitecturalBlogTranslates_ArchitecturalBlogs_Architectura~",
                        column: x => x.ArchitecturalBlogId,
                        principalTable: "ArchitecturalBlogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchitecturalBlogTranslates_ArchitecturalBlogId",
                table: "ArchitecturalBlogTranslates",
                column: "ArchitecturalBlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchitecturalBlogTranslates");

            migrationBuilder.DropTable(
                name: "ArchitecturalBlogs");
        }
    }
}
