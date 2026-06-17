using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionGamePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plans_PlanType",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlanType",
                table: "Plans");

            migrationBuilder.CreateTable(
                name: "SectionGamePlans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionGamePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionGamePlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionGamePlans_SectionGames_SectionId_GameId",
                        columns: x => new { x.SectionId, x.GameId },
                        principalTable: "SectionGames",
                        principalColumns: new[] { "SectionId", "GameId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionGamePlans_PlanId_SectionId_GameId_PlanType",
                table: "SectionGamePlans",
                columns: new[] { "PlanId", "SectionId", "GameId", "PlanType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionGamePlans_SectionId_GameId",
                table: "SectionGamePlans",
                columns: new[] { "SectionId", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionGamePlans");

            migrationBuilder.AddColumn<int>(
                name: "PlanType",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlanType",
                table: "Plans",
                column: "PlanType");
        }
    }
}
