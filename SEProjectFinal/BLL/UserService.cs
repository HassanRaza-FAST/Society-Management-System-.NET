using SEProjectFinal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEProjectFinal.BLL
{
    class UserService
    {
        private UserDataAccess userDataAccess;

        public UserService(string connectionString)
        {
            this.userDataAccess = new UserDataAccess(connectionString);
        }

        public bool AreCredentialsValid(string email, string password, string userType)
        {
            return userDataAccess.AreCredentialsValid(email, password, userType);
        }
    }

}
