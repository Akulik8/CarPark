namespace CarParkSystem.Data.Models
{
    public class Route
    {
        public Guid RouteID { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public float Distance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public float FuelConsumption { get; set; }
    }
}
