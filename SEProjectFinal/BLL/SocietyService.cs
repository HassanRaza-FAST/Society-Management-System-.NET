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

        public int CreateSociety(Society society)
        {
            return societyDataAccess.CreateSociety(society);
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

    }

}
