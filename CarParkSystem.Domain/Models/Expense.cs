namespace CarParkSystem.Data.Models
{
    public class Expense
    {
        public Guid ExpenseID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseType { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
    }
}
