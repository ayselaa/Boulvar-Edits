using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddGalleryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GalleryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryItemTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Tittle = table.Column<string>(type: "text", nullable: false),
                    GalleryItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryItemTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryItemTranslates_GalleryItems_GalleryItemId",
                        column: x => x.GalleryItemId,
                        principalTable: "GalleryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryItemTranslates_GalleryItemId",
                table: "GalleryItemTranslates",
                column: "GalleryItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryItemTranslates");

            migrationBuilder.DropTable(
                name: "GalleryItems");
        }
    }
}
