using SEProjectFinal.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal
{
    class SocietyService
    {
        private SocietyDataAccess societyDataAccess;

        public SocietyService(string connectionString)
        {
            this.societyDataAccess = new SocietyDataAccess(connectionString);
        }

        public DataTable GetAllSocieties()
        {
            return societyDataAccess.GetAllSocieties();
        }
        public int CreateSocietyApplication(SocietyApplication application)
        {
            return societyDataAccess.CreateSocietyApplication(application);
        }
        public DataTable GetSocietiesByStudentId(int studentId)
        {
            return societyDataAccess.GetSocietiesByStudentId(studentId);
        }
        public DataTable GetAllSocietyApplications()
        {
            return societyDataAccess.GetAllSocietyApplications();
        }

        public int UpdateApplicationStatus(int applicationID, string status)
        {
            return societyDataAccess.UpdateApplicationStatus(applicationID, status);
        }
        public SocietyApplication GetApplicationDetails(int applicationID)
        {
            return societyDataAccess.GetApplicationDetails(applicationID);
        }

        public int CreateSociety(Society society, int mentorID)
        {
            return societyDataAccess.CreateSociety(society, mentorID);
        }
        public int CreateSocietyMember(SocietyMember societyMember)
        {
            return societyDataAccess.CreateSocietyMember(societyMember);
        }

        public int CreateSocietyExecutive(SocietyExecutive societyExecutive)
        {
            return societyDataAccess.CreateSocietyExecutive(societyExecutive);
        }

        public bool DeleteSociety(int societyID)
        {
            return societyDataAccess.DeleteSociety(societyID);
        }

        public int CreateMembershipRequest(MembershipRequest application)
        {
            return societyDataAccess.CreateMembershipRequest(application);
        }
        public DataTable GetAllMembershipApplications()
        {
            return societyDataAccess.GetAllMembershipApplications();
        }
        public int UpdateMembershipStatus(int requestID, string status)
        {
            return societyDataAccess.UpdateMembershipStatus(requestID, status);
        }
        public MembershipRequest GetMembershipRequestDetails(int requestID)
        {
            return societyDataAccess.GetMembershipRequestDetails(requestID);
        }
        public int CreateEvent(Event newEvent)
        {
            return societyDataAccess.CreateEvent(newEvent);
        }
        public DataTable GetEventsByStatus(string status, int societyId)
        {
            return societyDataAccess.GetEventsByStatus(status, societyId);
        }
        public DataTable GetEvents(int societyId)
        {
            return societyDataAccess.GetEvents(societyId);
        }
        public SocietyExecutive GetSocietyExecutiveByStudentId(int studentId)
        {
            return societyDataAccess.GetSocietyExecutiveByStudentId(studentId);
        }
        public SocietyMember GetSocietyMemberByStudentId(int studentId)
        {
            return societyDataAccess.GetSocietyMemberByStudentId(studentId);
        }
        public void UpdateEventStatus(int eventId, string status)
        {
            societyDataAccess.UpdateEventStatus(eventId, status);
        }
        public int CreateAnnouncement(Announcement newAnnouncement)
        {
            return societyDataAccess.CreateAnnouncement(newAnnouncement);
        }
        public DataTable GetAnnouncementofExec(int studentId)
        {
            return societyDataAccess.GetAnnouncementofExec(studentId);
        }
        public DataTable GetAnnouncementsForJoinedSocieties(int studentId)
        {
            return societyDataAccess.GetAnnouncementsForJoinedSocieties(studentId);
        }
        public DataTable GetEventsForJoinedSocieties(int studentId)
        {
            return societyDataAccess.GetEventsForJoinedSocieties(studentId);
        }
        public DataTable GetAllMentors()
        {
            return societyDataAccess.GetAllMentors();
        }
        public DataTable GetAllStudents()
        {
            return societyDataAccess.GetAllStudents();
        }
    }

}
