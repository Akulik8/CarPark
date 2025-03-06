namespace CarParkSystem.Domain.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
