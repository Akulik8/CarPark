namespace CarParkSystem.Domain.Models
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
        public string Status { get; set; }

        public List<Trip> Trips { get; set; }
        public List<Accident> Accidents { get; set; }
        public List<Violation> Violations { get; set; }
        public List<WorkShift> WorkShifts { get; set; }
        //public List<VehicleAssignment> Assignments { get; set; }
    }
}