using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenSectionGameAndAcademicYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AcademicYears_AcademicYearId",
                table: "Exams");

            migrationBuilder.CreateTable(
                name: "SectionGameAcademicYears",
                columns: table => new
                {
                    SectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicYearId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionGameAcademicYears", x => new { x.SectionId, x.GameId, x.AcademicYearId });
                    table.ForeignKey(
                        name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionGameAcademicYears_SectionGames_SectionId_GameId",
                        columns: x => new { x.SectionId, x.GameId },
                        principalTable: "SectionGames",
                        principalColumns: new[] { "SectionId", "GameId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_TitleEn",
                table: "AcademicYears",
                column: "TitleEn");

            migrationBuilder.CreateIndex(
                name: "IX_SectionGameAcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears",
                column: "AcademicYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AcademicYears_AcademicYearId",
                table: "Exams",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AcademicYears_AcademicYearId",
                table: "Exams");

            migrationBuilder.DropTable(
                name: "SectionGameAcademicYears");

            migrationBuilder.DropIndex(
                name: "IX_AcademicYears_TitleEn",
                table: "AcademicYears");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AcademicYears_AcademicYearId",
                table: "Exams",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
