namespace CarParkSystem.Data.Models
{
    public class Driver
    {
        public Guid DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseCategory { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly EmploymentDate { get; set; }
    }
}