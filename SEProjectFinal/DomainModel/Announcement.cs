using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.DomainModel
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public int SocietyID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedByStudentID { get; set; }
        public DateTime CreatedDate { get; set; }

        public Announcement()
        {
            CreatedDate = DateTime.Now;
        }

        // Navigation properties
        public Society Society { get; set; }
        public Student CreatedByStudent { get; set; }
    }
}
