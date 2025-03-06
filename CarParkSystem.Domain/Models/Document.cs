using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Domain.Models
{
    public class Document
    {
        public Guid DocumentID { get; set; }
        public Guid VehicleID { get; set; }
        public string DocumentType { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public string FilePath { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
