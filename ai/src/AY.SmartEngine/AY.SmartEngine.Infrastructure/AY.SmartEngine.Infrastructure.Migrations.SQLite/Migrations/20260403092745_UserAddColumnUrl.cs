using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AY.SmartEngine.Infrastructure.Migrations.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class UserAddColumnUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Users");
        }
    }
}
