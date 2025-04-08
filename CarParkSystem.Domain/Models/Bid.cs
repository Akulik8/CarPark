using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Domain.Models
{
    public class Bid
    {
        public Guid BidID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DoDate { get; set; }
        public string Cargo { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public Guid SubdivisionID { get; set; }
        public Guid UserID { get; set; }

        public Subdivision Subdivision { get; set; }
        public User User { get; set; }
    }
}
