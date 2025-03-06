namespace CarParkSystem.Domain.Models
{
    public class Vehicle
    {
        public Guid VehicleID { get; set; }
        public string VehicleType { get; set; }
        public string VehicleCategory { get; set; }
        public string LicensePlate { get; set; }
        public string Make {  get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mass { get; set; }
        public int MaxMass { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; }
        public int NumberOfSeats { get; set; }
        public double FuelConsumption { get; set; }
        public string VIN { get; set; }
        public double Mileage { get; set; }
        public string FuelType { get; set; }
        public string Status { get; set; }

        public List<Trip> Trips { get; set; }
        public List<FuelRecord> FuelRecords { get; set; }
        public List<Maintenance> Maintenances { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<Accident> Accidents { get; set; }
        public List<Insurance> Insurances { get; set; }
        public List<Alert> Alerts { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Violation> Violations { get; set; }
        public List<Document> Documents { get; set; }
        public List<WorkShift> WorkShifts { get; set; }
        //public List<VehicleAssignment> Assignments { get; set; }
    }
}
