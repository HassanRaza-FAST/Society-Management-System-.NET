using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class Event
    {
        public int EventID { get; set; }
        public int SocietyID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int CreatedByStudentID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        // Constructor
        public Event()
        {
            CreatedDate = DateTime.Now;
            Status = "Pending";
        }
    }
}
