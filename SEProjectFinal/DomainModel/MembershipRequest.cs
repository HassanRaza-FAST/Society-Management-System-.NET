using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class MembershipRequest
    {
        public int RequestID { get; set; }
        public int StudentID { get; set; }
        public int SocietyID { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string DepartmentName { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }

        public MembershipRequest()
        {
            // Set default values.
            RequestDate = DateTime.Now;
            Status = "Pending";
        }
    }

}
