using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolGame.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    GameIsDraw = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Games__2AB897FD94753596", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ProfileName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserPassword = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C308E701E", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    StatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false),
                    ShotsMade = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    ShotsAttempted = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    HandBalls = table.Column<int>(type: "int", nullable: true),
                    Fouls = table.Column<int>(type: "int", nullable: true),
                    BestStreak = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlayerSt__3A162D3E5E4AB4AA", x => x.StatId);
                    table.ForeignKey(
                        name: "FK__PlayerSta__GameI__4316F928",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId");
                    table.ForeignKey(
                        name: "FK__PlayerSta__UserI__440B1D61",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StatsPerTurn",
                columns: table => new
                {
                    StatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    ShotsMade = table.Column<int>(type: "int", nullable: true),
                    ShotsAttempted = table.Column<int>(type: "int", nullable: true),
                    HandBalls = table.Column<int>(type: "int", nullable: true),
                    Fouls = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StatsPer__3A162D3E6F19A770", x => x.StatId);
                    table.ForeignKey(
                        name: "FK__StatsPerT__GameI__44FF419A",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId");
                    table.ForeignKey(
                        name: "FK__StatsPerT__Playe__45F365D3",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_UserId",
                table: "PlayerStats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_PlayerStats_GameUser",
                table: "PlayerStats",
                columns: new[] { "GameId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatsPerTurn_GameId",
                table: "StatsPerTurn",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsPerTurn_PlayerId",
                table: "StatsPerTurn",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E4112CF22B",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "StatsPerTurn");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
