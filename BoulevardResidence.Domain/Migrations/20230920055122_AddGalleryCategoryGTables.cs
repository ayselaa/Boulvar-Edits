using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddGalleryCategoryGTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryItemTranslates");

            migrationBuilder.AddColumn<int>(
                name: "GalleryCategoryId",
                table: "GalleryItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GalleryCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoftDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryCategoryTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Tittle = table.Column<string>(type: "text", nullable: false),
                    GalleryCategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryCategoryTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryCategoryTranslates_GalleryCategories_GalleryCategory~",
                        column: x => x.GalleryCategoryId,
                        principalTable: "GalleryCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryItems_GalleryCategoryId",
                table: "GalleryItems",
                column: "GalleryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryCategoryTranslates_GalleryCategoryId",
                table: "GalleryCategoryTranslates",
                column: "GalleryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItems_GalleryCategories_GalleryCategoryId",
                table: "GalleryItems",
                column: "GalleryCategoryId",
                principalTable: "GalleryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItems_GalleryCategories_GalleryCategoryId",
                table: "GalleryItems");

            migrationBuilder.DropTable(
                name: "GalleryCategoryTranslates");

            migrationBuilder.DropTable(
                name: "GalleryCategories");

            migrationBuilder.DropIndex(
                name: "IX_GalleryItems_GalleryCategoryId",
                table: "GalleryItems");

            migrationBuilder.DropColumn(
                name: "GalleryCategoryId",
                table: "GalleryItems");

            migrationBuilder.CreateTable(
                name: "GalleryItemTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GalleryItemId = table.Column<int>(type: "integer", nullable: true),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Tittle = table.Column<string>(type: "text", nullable: false)
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
    }
}
