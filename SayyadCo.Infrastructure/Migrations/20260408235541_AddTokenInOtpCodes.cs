using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenInOtpCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedAttempts",
                table: "OtpCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastResendAt",
                table: "OtpCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockedUntil",
                table: "OtpCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "OtpCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedAttempts",
                table: "OtpCodes");

            migrationBuilder.DropColumn(
                name: "LastResendAt",
                table: "OtpCodes");

            migrationBuilder.DropColumn(
                name: "LockedUntil",
                table: "OtpCodes");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "OtpCodes");
        }
    }
}
