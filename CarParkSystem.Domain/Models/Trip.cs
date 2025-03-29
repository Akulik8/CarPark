namespace CarParkSystem.Domain.Models
{
    public class Trip
    {
        public Guid TripID { get; set; }
        public Guid VehicleID { get; set; }
        public Guid DriverID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int MileageAtStart { get; set; }
        public int? MileageAtEnd { get; set; }
        public float? FuelUsed { get; set; }

        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public List<Route> Routes { get; set; }
    }
}
