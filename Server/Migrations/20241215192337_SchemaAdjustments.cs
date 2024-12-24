using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusTicketsApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class SchemaAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Timetables_RouteId",
                table: "Timetables");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Tickets",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Date_TimetableId",
                table: "Trips",
                columns: new[] { "Date", "TimetableId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_RouteId_DayOfWeek_TimeOfDeparture",
                table: "Timetables",
                columns: new[] { "RouteId", "DayOfWeek", "TimeOfDeparture" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_Date_TimetableId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Timetables_RouteId_DayOfWeek_TimeOfDeparture",
                table: "Timetables");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Tickets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_RouteId",
                table: "Timetables",
                column: "RouteId");
        }
    }
}
