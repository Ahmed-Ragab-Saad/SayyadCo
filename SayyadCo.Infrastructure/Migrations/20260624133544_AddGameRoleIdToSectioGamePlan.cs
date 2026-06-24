using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRoleIdToSectioGamePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SectionGamePlans_PlanId_SectionId_GameId_PlanType",
                table: "SectionGamePlans");

            migrationBuilder.DropColumn(
                name: "PlanType",
                table: "SectionGamePlans");

            migrationBuilder.AddColumn<string>(
                name: "GameRoleId",
                table: "SectionGamePlans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionGamePlanId",
                table: "Codes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SectionGamePlans_GameRoleId",
                table: "SectionGamePlans",
                column: "GameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionGamePlans_PlanId_SectionId_GameId_GameRoleId",
                table: "SectionGamePlans",
                columns: new[] { "PlanId", "SectionId", "GameId", "GameRoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codes_SectionGamePlanId",
                table: "Codes",
                column: "SectionGamePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_SectionGamePlans_SectionGamePlanId",
                table: "Codes",
                column: "SectionGamePlanId",
                principalTable: "SectionGamePlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGamePlans_GameRoles_GameRoleId",
                table: "SectionGamePlans",
                column: "GameRoleId",
                principalTable: "GameRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_SectionGamePlans_SectionGamePlanId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionGamePlans_GameRoles_GameRoleId",
                table: "SectionGamePlans");

            migrationBuilder.DropIndex(
                name: "IX_SectionGamePlans_GameRoleId",
                table: "SectionGamePlans");

            migrationBuilder.DropIndex(
                name: "IX_SectionGamePlans_PlanId_SectionId_GameId_GameRoleId",
                table: "SectionGamePlans");

            migrationBuilder.DropIndex(
                name: "IX_Codes_SectionGamePlanId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "GameRoleId",
                table: "SectionGamePlans");

            migrationBuilder.DropColumn(
                name: "SectionGamePlanId",
                table: "Codes");

            migrationBuilder.AddColumn<int>(
                name: "PlanType",
                table: "SectionGamePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SectionGamePlans_PlanId_SectionId_GameId_PlanType",
                table: "SectionGamePlans",
                columns: new[] { "PlanId", "SectionId", "GameId", "PlanType" },
                unique: true);
        }
    }
}
