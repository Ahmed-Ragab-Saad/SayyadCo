using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SayyadCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsExternalImageInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExternalImage",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExternalImage",
                table: "AspNetUsers");
        }
    }
}
