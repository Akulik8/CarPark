using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "FuelRecords");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Subdivisions",
                columns: table => new
                {
                    SubdivisionID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subdivisions", x => x.SubdivisionID);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    BidID = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DoDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cargo = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Volume = table.Column<double>(type: "double precision", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SubdivisionID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.BidID);
                    table.ForeignKey(
                        name: "FK_Bids_Subdivisions_SubdivisionID",
                        column: x => x.SubdivisionID,
                        principalTable: "Subdivisions",
                        principalColumn: "SubdivisionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_SubdivisionID",
                table: "Bids",
                column: "SubdivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_UserID",
                table: "Bids",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Subdivisions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverID = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    EmploymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LicenseCategory = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    LicenseNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    FuelConsumption = table.Column<double>(type: "double precision", nullable: false),
                    FuelType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Make = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Mass = table.Column<int>(type: "integer", nullable: false),
                    MaxMass = table.Column<int>(type: "integer", nullable: false),
                    Mileage = table.Column<double>(type: "double precision", nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NumberOfSeats = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    VIN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    VehicleCategory = table.Column<string>(type: "text", nullable: false),
                    VehicleType = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleID);
                });

            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    AccidentID = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    AccidentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccidentDetails = table.Column<string>(type: "text", nullable: false),
                    DamageCost = table.Column<float>(type: "real", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.AccidentID);
                    table.ForeignKey(
                        name: "FK_Accidents_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accidents_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    AlertType = table.Column<string>(type: "text", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_Documents_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpenseType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseID);
                    table.ForeignKey(
                        name: "FK_Expenses_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuelRecords",
                columns: table => new
                {
                    FuelRecordID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FuelAmount = table.Column<float>(type: "real", nullable: false),
                    FuelPrice = table.Column<float>(type: "real", nullable: false),
                    FuelStation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelRecords", x => x.FuelRecordID);
                    table.ForeignKey(
                        name: "FK_FuelRecords_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InsuranceCompany = table.Column<string>(type: "text", nullable: false),
                    InsuranceCost = table.Column<float>(type: "real", nullable: false),
                    PolicyNumber = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsuranceID);
                    table.ForeignKey(
                        name: "FK_Insurances_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    MaintenanceID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    ServiceCenter = table.Column<string>(type: "text", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.MaintenanceID);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    RepairID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    ProblemDescription = table.Column<string>(type: "text", nullable: false),
                    RepairCenter = table.Column<string>(type: "text", nullable: false),
                    RepairCost = table.Column<float>(type: "real", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RepairDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.RepairID);
                    table.ForeignKey(
                        name: "FK_Repairs_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripID = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FuelUsed = table.Column<float>(type: "real", nullable: true),
                    MileageAtEnd = table.Column<int>(type: "integer", nullable: true),
                    MileageAtStart = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripID);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    ViolationID = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    FineAmount = table.Column<float>(type: "real", nullable: false),
                    Paid = table.Column<bool>(type: "boolean", nullable: false),
                    ViolationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ViolationType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.ViolationID);
                    table.ForeignKey(
                        name: "FK_Violations_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Violations_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    ShiftID = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverID = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleID = table.Column<Guid>(type: "uuid", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => x.ShiftID);
                    table.ForeignKey(
                        name: "FK_WorkShifts_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkShifts_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteID = table.Column<Guid>(type: "uuid", nullable: false),
                    TripID = table.Column<Guid>(type: "uuid", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    EndPoint = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StartPoint = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK_Routes_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_DriverID",
                table: "Accidents",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_VehicleID",
                table: "Accidents",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_VehicleID",
                table: "Alerts",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_VehicleID",
                table: "Documents",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleID",
                table: "Expenses",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_FuelRecords_VehicleID",
                table: "FuelRecords",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_VehicleID",
                table: "Insurances",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_VehicleID",
                table: "MaintenanceRecords",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_VehicleID",
                table: "Repairs",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TripID",
                table: "Routes",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverID",
                table: "Trips",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleID",
                table: "Trips",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_DriverID",
                table: "Violations",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_VehicleID",
                table: "Violations",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_DriverID",
                table: "WorkShifts",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShifts_VehicleID",
                table: "WorkShifts",
                column: "VehicleID");
        }
    }
}
