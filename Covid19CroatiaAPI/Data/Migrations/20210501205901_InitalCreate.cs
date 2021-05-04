using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid19CroatiaAPI.Data.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyCovidOverviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalConfirmed = table.Column<int>(type: "int", nullable: false),
                    TotalRecovered = table.Column<int>(type: "int", nullable: false),
                    TotalDeaths = table.Column<int>(type: "int", nullable: false),
                    DailyNewConfirmed = table.Column<int>(type: "int", nullable: false),
                    DailyNewRecovered = table.Column<int>(type: "int", nullable: false),
                    DailyNewDeaths = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCovidOverviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCovidOverviews");
        }
    }
}
