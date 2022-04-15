using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.Data.Migrations
{
    public partial class UserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    ProfilePictureURL = table.Column<string>(type: "text", nullable: false),
                    CoverPictureURL = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    AboutMeHeader = table.Column<string>(type: "text", nullable: false),
                    AboutMeDescription = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Ocupation = table.Column<string>(type: "text", nullable: false),
                    Education = table.Column<string[]>(type: "text[]", nullable: false),
                    ImagesURL = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
