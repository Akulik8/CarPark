using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.WPF.ViewModel
{
    public class BidViewModel
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

        public Guid SubdivisionID { get; set; }               // хранится ID
        public string SubdivisionName { get; set; }           // отображается
    }
}
