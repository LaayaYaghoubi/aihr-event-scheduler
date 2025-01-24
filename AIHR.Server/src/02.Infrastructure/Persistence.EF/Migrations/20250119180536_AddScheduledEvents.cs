using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIHR.EventScheduler.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduledEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 70, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    DateRange_End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateRange_Start = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledEvents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledEvents");
        }
    }
}
