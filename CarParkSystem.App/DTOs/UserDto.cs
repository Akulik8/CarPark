using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.DTOs
{
    public class UserDto
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }
        public string Status { get; set; }
    }
}
