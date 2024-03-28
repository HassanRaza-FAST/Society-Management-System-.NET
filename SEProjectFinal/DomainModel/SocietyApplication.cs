using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class SocietyApplication
    {
        public int ApplicationID { get; set; }
        public int StudentID { get; set; }
        public string SocietyName { get; set; }
        public string Description { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        public string DepartmentName { get; set; }
    }

}
