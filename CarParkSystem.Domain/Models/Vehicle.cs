namespace CarParkSystem.Data.Models
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
    }
}
