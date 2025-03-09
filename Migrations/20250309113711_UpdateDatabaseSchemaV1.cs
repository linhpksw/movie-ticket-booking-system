using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G5_MovieTicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchemaV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenSeatId",
                table: "Showtimes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenSeatId",
                table: "Showtimes",
                type: "int",
                nullable: true);
        }
    }
}
