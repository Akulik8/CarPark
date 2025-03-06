namespace CarParkSystem.Domain.Models
{
    public class Violation
    {
        public Guid ViolationID { get; set; }
        public Guid DriverID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime ViolationDate { get; set; }
        public string ViolationType { get; set; }
        public float FineAmount { get; set; }
        public bool Paid { get; set; }

        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
