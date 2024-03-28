using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class SocietyMember
    {
        public int SocietyMemberID { get; set; }
        public int StudentID { get; set; }
        public int SocietyID { get; set; }
        public string TeamName { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
