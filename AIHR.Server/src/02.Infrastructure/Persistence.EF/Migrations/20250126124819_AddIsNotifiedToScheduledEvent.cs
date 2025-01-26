using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIHR.EventScheduler.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddIsNotifiedToScheduledEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotified",
                table: "ScheduledEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotified",
                table: "ScheduledEvents");
        }
    }
}
