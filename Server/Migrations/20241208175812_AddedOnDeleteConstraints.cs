using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusTicketsApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedOnDeleteConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Cities_CityId",
                table: "RouteStops");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Routes_RouteId",
                table: "Timetables");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Timetables_TimetableId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSeats_RouteStops_RouteId_Sequence",
                table: "TripSeats");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ShortName",
                table: "Routes",
                column: "ShortName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Cities_CityId",
                table: "RouteStops",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Routes_RouteId",
                table: "Timetables",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Timetables_TimetableId",
                table: "Trips",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSeats_RouteStops_RouteId_Sequence",
                table: "TripSeats",
                columns: new[] { "RouteId", "Sequence" },
                principalTable: "RouteStops",
                principalColumns: new[] { "RouteId", "Sequence" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSeats_Trips_TripId",
                table: "TripSeats",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Cities_CityId",
                table: "RouteStops");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Routes_RouteId",
                table: "Timetables");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Timetables_TimetableId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSeats_RouteStops_RouteId_Sequence",
                table: "TripSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSeats_Trips_TripId",
                table: "TripSeats");

            migrationBuilder.DropIndex(
                name: "IX_Routes_ShortName",
                table: "Routes");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Cities_CityId",
                table: "RouteStops",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Routes_RouteId",
                table: "Timetables",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Timetables_TimetableId",
                table: "Trips",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSeats_RouteStops_RouteId_Sequence",
                table: "TripSeats",
                columns: new[] { "RouteId", "Sequence" },
                principalTable: "RouteStops",
                principalColumns: new[] { "RouteId", "Sequence" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
