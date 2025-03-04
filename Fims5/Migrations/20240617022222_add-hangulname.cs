using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fims5.Migrations
{
    /// <inheritdoc />
    public partial class addhangulname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HangulName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HangulName",
                table: "AspNetUsers");
        }
    }
}
