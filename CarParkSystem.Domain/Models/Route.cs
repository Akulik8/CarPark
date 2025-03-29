namespace CarParkSystem.Domain.Models
{
    public class Route
    {
        public Guid RouteID { get; set; }
        public Guid TripID { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public float Distance { get; set; }
        //public TimeSpan EstimatedTime { get; set; }
        //public float FuelConsumption { get; set; }

        public Trip Trip { get; set; }
    }
}
