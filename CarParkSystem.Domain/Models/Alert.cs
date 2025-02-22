namespace CarParkSystem.Data.Models
{
    public class Alert
    {
        public Guid AlertID { get; set; }
        public Guid VehicleID { get; set; }
        public string AlertType { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
