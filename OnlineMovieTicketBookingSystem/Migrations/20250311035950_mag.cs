using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMovieTicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class mag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TheatreName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TheatreName",
                table: "Tickets");
        }
    }
}
