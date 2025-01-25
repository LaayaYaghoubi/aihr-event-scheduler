using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIHR.EventScheduler.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToScheduledEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ScheduledEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ScheduledEvents");
        }
    }
}
