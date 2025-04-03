using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Domain.Models
{
    public class Subdivision
    {
        public Guid SubdivisionID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }

        public List<Bid> Bids { get; set; }
    }
}
