using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLApi.Migrations
{
    /// <inheritdoc />
    public partial class LoLDb5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RankedInfo",
                columns: table => new
                {
                    Puuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeagueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueueType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaguePoints = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankedInfo", x => x.Puuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankedInfo");
        }
    }
}
