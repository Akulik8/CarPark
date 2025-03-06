namespace CarParkSystem.Domain.Models
{
    public class Insurance
    {
        public Guid InsuranceID { get; set; }
        public Guid VehicleID { get; set; }
        public string InsuranceCompany { get; set; }
        public string PolicyNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public float InsuranceCost { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
