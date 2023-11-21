using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddTablesForComfort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComfortBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfortBlogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comforts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoftDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comforts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComfortBlogTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ComfortBlogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfortBlogTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComfortBlogTranslates_ComfortBlogs_ComfortBlogId",
                        column: x => x.ComfortBlogId,
                        principalTable: "ComfortBlogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComfortTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: false),
                    ComfortId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfortTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComfortTranslates_Comforts_ComfortId",
                        column: x => x.ComfortId,
                        principalTable: "Comforts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComfortBlogTranslates_ComfortBlogId",
                table: "ComfortBlogTranslates",
                column: "ComfortBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_ComfortTranslates_ComfortId",
                table: "ComfortTranslates",
                column: "ComfortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComfortBlogTranslates");

            migrationBuilder.DropTable(
                name: "ComfortTranslates");

            migrationBuilder.DropTable(
                name: "ComfortBlogs");

            migrationBuilder.DropTable(
                name: "Comforts");
        }
    }
}
