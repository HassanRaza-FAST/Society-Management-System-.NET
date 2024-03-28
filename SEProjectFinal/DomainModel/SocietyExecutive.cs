using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class SocietyExecutive
    {
        public int ExecutiveID { get; set; }
        public int SocietyID { get; set; }
        public int StudentID { get; set; }
        public string Position { get; set; }
        public int SocietyMemberID { get; set; }
    }
}
