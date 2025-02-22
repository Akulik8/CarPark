namespace CarParkSystem.Data.Models
{
    public class WorkShift
    {
        public Guid ShiftID { get; set; }
        public Guid DriverID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
