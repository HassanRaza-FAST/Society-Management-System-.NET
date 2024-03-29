using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProjectFinal.DAL
{
    class UserDataAccess
    {
        private string connectionString;

        public UserDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AreCredentialsValid(string email, string password, string userType)
        {
            // Implement your SQL query to check if the credentials are valid of the type of user
            // Return true if they are, false otherwise
            // write the query for user type
            string query;
            if(userType == "Admin")
            {
                query = "SELECT * FROM Administrators WHERE Email = @Email AND Password = @Password";
            }
            else if(userType == "Mentor")
            {
                query = "SELECT * FROM Mentors WHERE Email = @Email AND Password = @Password";
            }
            else
            {
                MessageBox.Show("Invalid User Type");
                return false;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }   

        }

    }

}
