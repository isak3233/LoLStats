using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLApi.Migrations
{
    /// <inheritdoc />
    public partial class LoLAppDb7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoLMatch",
                columns: table => new
                {
                    Puuid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoLMatch", x => x.Puuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoLMatch");
        }
    }
}
