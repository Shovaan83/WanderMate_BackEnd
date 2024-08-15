using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class bookingss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackages_Bookings_BookingId",
                table: "TravelPackages");

            migrationBuilder.DropIndex(
                name: "IX_TravelPackages_BookingId",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "TravelPackages");

            migrationBuilder.AddColumn<int>(
                name: "TravelPackagesId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingTravelPackages",
                columns: table => new
                {
                    BookingsBookingId = table.Column<int>(type: "int", nullable: false),
                    TravelPackagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTravelPackages", x => new { x.BookingsBookingId, x.TravelPackagesId });
                    table.ForeignKey(
                        name: "FK_BookingTravelPackages_Bookings_BookingsBookingId",
                        column: x => x.BookingsBookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingTravelPackages_TravelPackages_TravelPackagesId",
                        column: x => x.TravelPackagesId,
                        principalTable: "TravelPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TravelPackagesId",
                table: "Reviews",
                column: "TravelPackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTravelPackages_TravelPackagesId",
                table: "BookingTravelPackages",
                column: "TravelPackagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TravelPackages_TravelPackagesId",
                table: "Reviews",
                column: "TravelPackagesId",
                principalTable: "TravelPackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TravelPackages_TravelPackagesId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "BookingTravelPackages");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TravelPackagesId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "TravelPackagesId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "TravelPackages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_BookingId",
                table: "TravelPackages",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackages_Bookings_BookingId",
                table: "TravelPackages",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");
        }
    }
}
