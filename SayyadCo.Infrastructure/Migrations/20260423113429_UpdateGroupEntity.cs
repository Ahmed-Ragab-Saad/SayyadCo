using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGroupEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SectionId_GameId",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYearId",
                table: "Groups",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Exams",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByUserId_SectionId_GameId_AcademicYearId_Semester",
                table: "Groups",
                columns: new[] { "CreatedByUserId", "SectionId", "GameId", "AcademicYearId", "Semester" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SectionId_GameId_AcademicYearId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId", "AcademicYearId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_SectionGameAcademicYears_SectionId_GameId_AcademicYearId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId", "AcademicYearId" },
                principalTable: "SectionGameAcademicYears",
                principalColumns: new[] { "SectionId", "GameId", "AcademicYearId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_SectionGameAcademicYears_SectionId_GameId_AcademicYearId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatedByUserId_SectionId_GameId_AcademicYearId_Semester",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SectionId_GameId_AcademicYearId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Exams");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Exams",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SectionId_GameId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
