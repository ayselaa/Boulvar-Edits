using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class CreateManyToManyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feature_Apartments_ApartmentId",
                table: "Feature");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureTranslate_Feature_FeatureId",
                table: "FeatureTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureTranslate",
                table: "FeatureTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_ApartmentId",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Feature");

            migrationBuilder.RenameTable(
                name: "FeatureTranslate",
                newName: "FeatureTranslates");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureTranslate_FeatureId",
                table: "FeatureTranslates",
                newName: "IX_FeatureTranslates_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureTranslates",
                table: "FeatureTranslates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FeatureApartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApartmentId = table.Column<int>(type: "integer", nullable: false),
                    FeatureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureApartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureApartments_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureApartments_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureApartments_ApartmentId",
                table: "FeatureApartments",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureApartments_FeatureId",
                table: "FeatureApartments",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureTranslates_Features_FeatureId",
                table: "FeatureTranslates",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureTranslates_Features_FeatureId",
                table: "FeatureTranslates");

            migrationBuilder.DropTable(
                name: "FeatureApartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureTranslates",
                table: "FeatureTranslates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.RenameTable(
                name: "FeatureTranslates",
                newName: "FeatureTranslate");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureTranslates_FeatureId",
                table: "FeatureTranslate",
                newName: "IX_FeatureTranslate_FeatureId");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Feature",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureTranslate",
                table: "FeatureTranslate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ApartmentId",
                table: "Feature",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Apartments_ApartmentId",
                table: "Feature",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureTranslate_Feature_FeatureId",
                table: "FeatureTranslate",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id");
        }
    }
}
