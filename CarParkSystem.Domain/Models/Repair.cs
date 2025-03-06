namespace CarParkSystem.Domain.Models
{
    public class Repair
    {
        public Guid RepairID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime RepairDate { get; set; }
        public string ProblemDescription { get; set; }
        public string RepairDetails { get; set; }
        public float RepairCost { get; set; }
        public string RepairCenter { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
