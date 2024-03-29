using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class Mentor
    {
        public int MentorID { get; set; }
        public string Specialization { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public int SocietyID { get; set; }
    }

}
