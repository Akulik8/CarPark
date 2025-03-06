namespace CarParkSystem.Domain.Models
{
    public class WorkShift
    {
        public Guid ShiftID { get; set; }
        public Guid DriverID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
