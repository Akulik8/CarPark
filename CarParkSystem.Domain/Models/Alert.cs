namespace CarParkSystem.Domain.Models
{
    public class Alert
    {
        public Guid AlertID { get; set; }
        public Guid VehicleID { get; set; }
        public string AlertType { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public bool IsRead{ get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
