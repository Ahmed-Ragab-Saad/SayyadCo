using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGameRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_SectionGames_SectionId_GameId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams");

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
                name: "FK_Tests_Groups_GroupId",
                table: "Tests");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Tests",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "GameId",
                table: "Tests",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                table: "Tests",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "GameId",
                table: "Exams",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                table: "Exams",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserGameRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameRoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameRoles", x => new { x.UserId, x.GameRoleId, x.SectionId });
                    table.ForeignKey(
                        name: "FK_UserGameRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGameRoles_GameRoles_GameRoleId",
                        column: x => x.GameRoleId,
                        principalTable: "GameRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGameRoles_SectionGames_SectionId_GameId",
                        columns: x => new { x.SectionId, x.GameId },
                        principalTable: "SectionGames",
                        principalColumns: new[] { "SectionId", "GameId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SectionId_GameId",
                table: "Tests",
                columns: new[] { "SectionId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SectionId_GameId",
                table: "Exams",
                columns: new[] { "SectionId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGameRoles_GameRoleId",
                table: "UserGameRoles",
                column: "GameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameRoles_SectionId_GameId",
                table: "UserGameRoles",
                columns: new[] { "SectionId", "GameId" });

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_SectionGames_SectionId_GameId",
                table: "Exams",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
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
                name: "FK_Tests_Groups_GroupId",
                table: "Tests",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_SectionGames_SectionId_GameId",
                table: "Tests",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_SectionGames_SectionId_GameId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_SectionGames_SectionId_GameId",
                table: "Exams");

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
                name: "FK_Tests_Groups_GroupId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_SectionGames_SectionId_GameId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "UserGameRoles");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SectionId_GameId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SectionId_GameId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Exams");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Tests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                name: "FK_Groups_SectionGames_SectionId_GameId",
                table: "Groups",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Tests_Groups_GroupId",
                table: "Tests",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
