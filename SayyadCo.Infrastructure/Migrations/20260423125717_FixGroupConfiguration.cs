using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixGroupConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatedByUserId_SectionId_GameId_AcademicYearId_Semester",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByUserId_SectionId_GameId_AcademicYearId_Semester",
                table: "Groups",
                columns: new[] { "CreatedByUserId", "SectionId", "GameId", "AcademicYearId", "Semester" },
                unique: true);
        }
    }
}
