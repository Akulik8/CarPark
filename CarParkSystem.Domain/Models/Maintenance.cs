namespace CarParkSystem.Domain.Models
{
    public class Maintenance
    {
        public Guid MaintenanceID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceType { get; set; }
        public float Cost { get; set; }
        public string ServiceCenter { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
