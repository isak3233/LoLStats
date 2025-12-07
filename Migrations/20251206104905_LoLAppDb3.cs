using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLApi.Migrations
{
    /// <inheritdoc />
    public partial class LoLAppDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "SummonerAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "SummonerAccounts");
        }
    }
}
