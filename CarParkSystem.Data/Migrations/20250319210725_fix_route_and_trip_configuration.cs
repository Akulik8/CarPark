using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix_route_and_trip_configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_RouteID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_RouteID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "RouteID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "FuelConsumption",
                table: "Routes");

            migrationBuilder.AlterColumn<int>(
                name: "MileageAtEnd",
                table: "Trips",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<float>(
                name: "FuelUsed",
                table: "Trips",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "TripID",
                table: "Routes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TripID",
                table: "Routes",
                column: "TripID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trips_TripID",
                table: "Routes",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trips_TripID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_TripID",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TripID",
                table: "Routes");

            migrationBuilder.AlterColumn<int>(
                name: "MileageAtEnd",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "FuelUsed",
                table: "Trips",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RouteID",
                table: "Trips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EstimatedTime",
                table: "Routes",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<float>(
                name: "FuelConsumption",
                table: "Routes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteID",
                table: "Trips",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_RouteID",
                table: "Trips",
                column: "RouteID",
                principalTable: "Routes",
                principalColumn: "RouteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
