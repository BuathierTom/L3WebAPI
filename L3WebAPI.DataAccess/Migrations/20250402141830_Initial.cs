using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L3WebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    app_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.app_id);
                });

            migrationBuilder.CreateTable(
                name: "PriceDAO",
                columns: table => new
                {
                    currency = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    valeur = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDAO", x => new { x.GameId, x.currency });
                    table.ForeignKey(
                        name: "FK_PriceDAO_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "app_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceDAO");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
