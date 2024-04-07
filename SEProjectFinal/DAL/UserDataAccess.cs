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
            string query;
            if (userType == "Admin")
            {
                query = "SELECT * FROM Administrators WHERE Email = @Email AND Password = @Password";
            }
            else if (userType == "Mentor")
            {
                query = "SELECT * FROM Mentors WHERE Email = @Email AND Password = @Password AND SocietyID IS NOT NULL";
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
        // get mentor by email
        public DomainModel.Mentor GetMentor(string email)
        {
            DomainModel.Mentor mentor = new DomainModel.Mentor();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Mentors WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            mentor.MentorID = reader.GetInt32(reader.GetOrdinal("MentorID"));
                            mentor.Specialization = reader.GetString(reader.GetOrdinal("Specialization"));
                            mentor.Username = reader.GetString(reader.GetOrdinal("Username"));
                            mentor.Password = reader.GetString(reader.GetOrdinal("Password"));
                            mentor.Email = reader.GetString(reader.GetOrdinal("Email"));
                            mentor.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                            mentor.DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName"));
                            mentor.SocietyID = reader.GetInt32(reader.GetOrdinal("SocietyID"));
                        }
                    }
                }
            }
            return mentor;
        }

    }

}
