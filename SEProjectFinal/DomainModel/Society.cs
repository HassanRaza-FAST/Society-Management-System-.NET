using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class Society
    {
        public int SocietyID { get; set; }
        public string SocietyName { get; set; }
        public string Description { get; set; }
        public int CreatedByStudentID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DepartmentName { get; set; }
    }

}
