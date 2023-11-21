using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoulevardResidence.Domain.Migrations
{
    public partial class AddInfrastructureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Infrastructures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infrastructures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfrastructureTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    InfrastructureId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfrastructureTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfrastructureTranslates_Infrastructures_InfrastructureId",
                        column: x => x.InfrastructureId,
                        principalTable: "Infrastructures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfrastructureTranslates_InfrastructureId",
                table: "InfrastructureTranslates",
                column: "InfrastructureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfrastructureTranslates");

            migrationBuilder.DropTable(
                name: "Infrastructures");
        }
    }
}
