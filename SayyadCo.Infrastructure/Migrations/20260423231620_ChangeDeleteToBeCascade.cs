using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteToBeCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_GameRoles_GameRoleId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Codes_SectionGames_SectionId_GameId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_SectionGameAcademicYears_SectionId_GameId_AcademicYearId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_SectionGames_SectionId_GameId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_OtpCodes_AspNetUsers_UserId",
                table: "OtpCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGames_Games_GameId",
                table: "SectionGames");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGames_Sections_SectionId",
                table: "SectionGames");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGames_SectionGames_SectionId_GameId",
                table: "StudentGames");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherGames_SectionGames_SectionId_GameId",
                table: "TeacherGames");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AcademicYears_AcademicYearId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_SectionGames_SectionId_GameId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGameRoles_SectionGames_SectionId_GameId",
                table: "UserGameRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_GameRoles_GameRoleId",
                table: "Codes",
                column: "GameRoleId",
                principalTable: "GameRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_SectionGames_SectionId_GameId",
                table: "Codes",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_SectionGameAcademicYears_SectionId_GameId_AcademicYearId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId", "AcademicYearId" },
                principalTable: "SectionGameAcademicYears",
                principalColumns: new[] { "SectionId", "GameId", "AcademicYearId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_SectionGames_SectionId_GameId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OtpCodes_AspNetUsers_UserId",
                table: "OtpCodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGames_Games_GameId",
                table: "SectionGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGames_Sections_SectionId",
                table: "SectionGames",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGames_SectionGames_SectionId_GameId",
                table: "StudentGames",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherGames_SectionGames_SectionId_GameId",
                table: "TeacherGames",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AcademicYears_AcademicYearId",
                table: "Tests",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_SectionGames_SectionId_GameId",
                table: "Tests",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGameRoles_SectionGames_SectionId_GameId",
                table: "UserGameRoles",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_GameRoles_GameRoleId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Codes_SectionGames_SectionId_GameId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_SectionGameAcademicYears_SectionId_GameId_AcademicYearId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_SectionGames_SectionId_GameId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_OtpCodes_AspNetUsers_UserId",
                table: "OtpCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGames_Games_GameId",
                table: "SectionGames");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGames_Sections_SectionId",
                table: "SectionGames");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGames_SectionGames_SectionId_GameId",
                table: "StudentGames");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherGames_SectionGames_SectionId_GameId",
                table: "TeacherGames");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AcademicYears_AcademicYearId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_SectionGames_SectionId_GameId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGameRoles_SectionGames_SectionId_GameId",
                table: "UserGameRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_GameRoles_GameRoleId",
                table: "Codes",
                column: "GameRoleId",
                principalTable: "GameRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_SectionGames_SectionId_GameId",
                table: "Codes",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_SectionGameAcademicYears_SectionId_GameId_AcademicYearId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId", "AcademicYearId" },
                principalTable: "SectionGameAcademicYears",
                principalColumns: new[] { "SectionId", "GameId", "AcademicYearId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_SectionGames_SectionId_GameId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtpCodes_AspNetUsers_UserId",
                table: "OtpCodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGameAcademicYears_AcademicYears_AcademicYearId",
                table: "SectionGameAcademicYears",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGames_Games_GameId",
                table: "SectionGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGames_Sections_SectionId",
                table: "SectionGames",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGames_SectionGames_SectionId_GameId",
                table: "StudentGames",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherGames_SectionGames_SectionId_GameId",
                table: "TeacherGames",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AcademicYears_AcademicYearId",
                table: "Tests",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_SectionGames_SectionId_GameId",
                table: "Tests",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGameRoles_SectionGames_SectionId_GameId",
                table: "UserGameRoles",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
