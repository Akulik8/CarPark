namespace CarParkSystem.Data.Models
{
    public class FuelRecord
    {
        public Guid FuelRecordID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime Date { get; set; }
        public float FuelAmount { get; set; }
        public float FuelPrice { get; set; }
        public string FuelStation { get; set; }
    }
}
