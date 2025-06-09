using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerPortal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Coach = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HomeCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FoundedYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    GoalsScored = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team1ID = table.Column<int>(type: "int", nullable: false),
                    Team2ID = table.Column<int>(type: "int", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchID);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team1ID",
                        column: x => x.Team1ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team2ID",
                        column: x => x.Team2ID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    FixtureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    FixtureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.FixtureID);
                    table.ForeignKey(
                        name: "FK_Fixtures_Matches_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matches",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_MatchID",
                table: "Fixtures",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1ID",
                table: "Matches",
                column: "Team1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2ID",
                table: "Matches",
                column: "Team2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_VenueID",
                table: "Matches",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamID",
                table: "Players",
                column: "TeamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
