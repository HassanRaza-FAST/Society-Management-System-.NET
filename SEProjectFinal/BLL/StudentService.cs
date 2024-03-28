using SEProjectFinal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.BLL
{
    class StudentService
    {
        private StudentDataAccess studentDataAccess;

        public StudentService(string connectionString)
        {
            this.studentDataAccess = new StudentDataAccess(connectionString);
        }

        public Student GetStudentByEmailAndPassword(string email, string password)
        {
            return studentDataAccess.GetStudentByEmailAndPassword(email, password);
        }

        public int? GetSocietyMemberIdByStudentId(int studentId)
        {
            return studentDataAccess.GetSocietyMemberIdByStudentId(studentId);
        }

        public bool IsSocietyExecutive(int societyMemberId)
        {
            return studentDataAccess.IsSocietyExecutive(societyMemberId);
        }
    }

}
