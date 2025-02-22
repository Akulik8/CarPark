namespace CarParkSystem.Data.Models
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
    }
}
