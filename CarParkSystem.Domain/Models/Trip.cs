namespace CarParkSystem.Data.Models
{
    public class Trip
    {
        public Guid TripID { get; set; }
        public Guid VehicleID { get; set; }
        public Guid DriverID { get; set; }
        public Guid RouteID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MileageAtStart { get; set; }
        public int MileageAtEnd { get; set; }
        public float FuelUsed { get; set; }
    }
}
