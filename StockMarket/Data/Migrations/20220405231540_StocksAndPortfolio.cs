using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockMarket.Data.Migrations
{
    public partial class StocksAndPortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockBought",
                columns: table => new
                {
                    StocksBoughtId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    StockName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    QuantityBought = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBought", x => x.StocksBoughtId);
                });

            migrationBuilder.CreateTable(
                name: "UserPortfolios",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    StockName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    StockPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPortfolios", x => new { x.Email, x.StockName });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockBought");

            migrationBuilder.DropTable(
                name: "UserPortfolios");
        }
    }
}
