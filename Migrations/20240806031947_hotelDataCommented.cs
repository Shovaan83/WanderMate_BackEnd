﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWeb.Migrations
{
    /// <inheritdoc />
    public partial class hotelDataCommented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Hotel_HotelId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Hotel_HotelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Hotel_HotelId",
                table: "Bookings",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Hotel_HotelId",
                table: "Reviews",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id");
        }
    }
}
