using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserGameErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_AspNetUsers_ApplicationUserId",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_SectionGames_SectionGameSectionId_SectionGameGameId",
                table: "UserGames");

            migrationBuilder.DropIndex(
                name: "IX_UserGames_ApplicationUserId",
                table: "UserGames");

            migrationBuilder.DropIndex(
                name: "IX_UserGames_SectionGameSectionId_SectionGameGameId",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "SectionGameGameId",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "SectionGameSectionId",
                table: "UserGames");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserGames",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "UserGames",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                table: "UserGames",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "UserGames",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsExternalImage",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_SectionId_GameId",
                table: "UserGames",
                columns: new[] { "SectionId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_AspNetUsers_UserId",
                table: "UserGames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_SectionGames_SectionId_GameId",
                table: "UserGames",
                columns: new[] { "SectionId", "GameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_AspNetUsers_UserId",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_SectionGames_SectionId_GameId",
                table: "UserGames");

            migrationBuilder.DropIndex(
                name: "IX_UserGames_SectionId_GameId",
                table: "UserGames");

            migrationBuilder.DropIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "UserGames",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserGames",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionGameGameId",
                table: "UserGames",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionGameSectionId",
                table: "UserGames",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsExternalImage",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_ApplicationUserId",
                table: "UserGames",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_SectionGameSectionId_SectionGameGameId",
                table: "UserGames",
                columns: new[] { "SectionGameSectionId", "SectionGameGameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_AspNetUsers_ApplicationUserId",
                table: "UserGames",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_SectionGames_SectionGameSectionId_SectionGameGameId",
                table: "UserGames",
                columns: new[] { "SectionGameSectionId", "SectionGameGameId" },
                principalTable: "SectionGames",
                principalColumns: new[] { "SectionId", "GameId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
