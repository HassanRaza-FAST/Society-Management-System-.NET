using Microsoft.Data.SqlClient;
using System;

namespace SEProjectFinal.DAL
{
    class StudentDataAccess
    {
        private string connectionString;

        public StudentDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Student GetStudentByEmailAndPassword(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Students WHERE Email = @Email AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Student student = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Email = reader.GetString(3),
                                FullName = reader.GetString(4)
                            };
                            return student;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public int? GetSocietyMemberIdByStudentId(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT SocietyMemberID FROM SocietyMembers WHERE StudentID = @studentId", connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public bool IsSocietyExecutive(int societyMemberId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyExecutives WHERE SocietyMemberID = @societyMemberId", connection))
                {
                    command.Parameters.AddWithValue("@societyMemberId", societyMemberId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}
