using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLApi.Migrations
{
    /// <inheritdoc />
    public partial class LoLAppDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SummonerAccounts",
                columns: table => new
                {
                    Puuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileIconId = table.Column<int>(type: "int", nullable: false),
                    SummonerLevel = table.Column<int>(type: "int", nullable: false),
                    RevisionDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerAccounts", x => x.Puuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SummonerAccounts");
        }
    }
}
