namespace CarParkSystem.Domain.Models
{
    public class Accident
    {
        public Guid AccidentID { get; set; }
        public Guid VehicleID { get; set; }
        public Guid DriverID { get; set; }
        public DateTime AccidentDate { get; set; }
        public string Location { get; set; }
        public string AccidentDetails { get; set; }
        public float DamageCost { get; set; }

        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
    }
}
