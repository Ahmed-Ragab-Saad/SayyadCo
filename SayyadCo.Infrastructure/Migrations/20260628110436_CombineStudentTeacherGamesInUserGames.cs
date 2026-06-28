using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CombineStudentTeacherGamesInUserGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTypes");

            migrationBuilder.DropTable(
                name: "StudentGames");

            migrationBuilder.DropTable(
                name: "TeacherGames");

            migrationBuilder.DropTable(
                name: "UserGameRoles");

            migrationBuilder.CreateTable(
                name: "UserGames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameRoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionGameSectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionGameGameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGames_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGames_GameRoles_GameRoleId",
                        column: x => x.GameRoleId,
                        principalTable: "GameRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGames_SectionGames_SectionGameSectionId_SectionGameGameId",
                        columns: x => new { x.SectionGameSectionId, x.SectionGameGameId },
                        principalTable: "SectionGames",
                        principalColumns: new[] { "SectionId", "GameId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_ApplicationUserId",
                table: "UserGames",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_GameRoleId",
                table: "UserGames",
                column: "GameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_SectionGameSectionId_SectionGameGameId",
                table: "UserGames",
                columns: new[] { "SectionGameSectionId", "SectionGameGameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGames");

            migrationBuilder.CreateTable(
                name: "GameTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentGames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGames_SectionGames_SectionId_GameId",
                        columns: x => new { x.SectionId, x.GameId },
                        principalTable: "SectionGames",
                        principalColumns: new[] { "SectionId", "GameId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGames_SectionGames_SectionId_GameId",
                        columns: x => new { x.SectionId, x.GameId },
                        principalTable: "SectionGames",
                        principalColumns: new[] { "SectionId", "GameId" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGames_SectionId_GameId",
                table: "StudentGames",
                columns: new[] { "SectionId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGames_UserId",
                table: "StudentGames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGames_SectionId_GameId",
                table: "TeacherGames",
                columns: new[] { "SectionId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGames_UserId",
                table: "TeacherGames",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameRoles_GameRoleId",
                table: "UserGameRoles",
                column: "GameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameRoles_SectionId_GameId",
                table: "UserGameRoles",
                columns: new[] { "SectionId", "GameId" });
        }
    }
}
