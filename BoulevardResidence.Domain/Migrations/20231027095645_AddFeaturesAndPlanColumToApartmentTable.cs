using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddFeaturesAndPlanColumToApartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApartmentPlan",
                table: "Apartments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Logo = table.Column<string>(type: "text", nullable: false),
                    ApartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeatureTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FeatureId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureTranslate_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ApartmentId",
                table: "Feature",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureTranslate_FeatureId",
                table: "FeatureTranslate",
                column: "FeatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureTranslate");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropColumn(
                name: "ApartmentPlan",
                table: "Apartments");
        }
    }
}
