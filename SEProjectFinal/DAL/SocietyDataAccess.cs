using Azure.Core;
using Microsoft.Data.SqlClient;
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
    class SocietyDataAccess
    {
        private string connectionString;

        public SocietyDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetAllSocieties()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Societies", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
        public int CreateSocietyApplication(SocietyApplication application)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // first get the number of number of rows in the table SocietyCreationApplications
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM SocietyCreationApplications", connection))
                {
                    int count = (int)command1.ExecuteScalar();
                    count++;

                    // insert the form into the database
                    using (SqlCommand command2 = new SqlCommand("INSERT INTO SocietyCreationApplications (ApplicationID, StudentID, SocietyName, DepartmentName, Description) VALUES (@ApplicationID, @StudentID, @SocietyName, @DepartmentName, @Description)", connection))
                    {
                        command2.Parameters.AddWithValue("@ApplicationID", count);
                        command2.Parameters.AddWithValue("@StudentID", application.StudentID);
                        command2.Parameters.AddWithValue("@SocietyName", application.SocietyName);
                        command2.Parameters.AddWithValue("@DepartmentName", application.DepartmentName);
                        command2.Parameters.AddWithValue("@Description", application.Description);

                        int rowsAffected = command2.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return count; // return the ApplicationID of the newly created application
                        }
                        else
                        {
                            return -1; // return -1 to indicate that the insertion failed
                        }
                    }
                }
            }
        }
        public DataTable GetSocietiesByStudentId(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Societies WHERE SocietyID IN (SELECT SocietyID FROM SocietyMembers WHERE StudentID = @studentId)", connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
        public DataTable GetAllSocietyApplications()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyCreationApplications", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
        public DataTable GetAllMembershipApplications()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MembershipRequests", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public int UpdateApplicationStatus(int applicationID, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE SocietyCreationApplications SET Status = @Status WHERE ApplicationID = @ApplicationID", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@ApplicationID", applicationID);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public SocietyApplication GetApplicationDetails(int applicationID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyCreationApplications WHERE ApplicationID = @ApplicationID", connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", applicationID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return new SocietyApplication
                            {
                                ApplicationID = reader.GetInt32(0),
                                StudentID = reader.GetInt32(1),
                                SocietyName = reader.GetString(2),
                                Description = reader.GetString(3),
                                DepartmentName = reader.GetString(6)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public int CreateSociety(Society society)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Societies", connection))
                {
                    int societyCount = (int)command.ExecuteScalar();
                    societyCount++; // Increment the count to get the new SocietyID

                    command.CommandText = "INSERT INTO Societies (SocietyID, SocietyName, Description, CreatedByStudentID, DepartmentName) VALUES (@SocietyID, @SocietyName, @Description, @CreatedByStudentID, @DepartmentName)";
                    command.Parameters.AddWithValue("@SocietyID", societyCount);
                    command.Parameters.AddWithValue("@SocietyName", society.SocietyName);
                    command.Parameters.AddWithValue("@Description", society.Description);
                    command.Parameters.AddWithValue("@CreatedByStudentID", society.CreatedByStudentID);
                    command.Parameters.AddWithValue("@DepartmentName", society.DepartmentName);
                    int rowsInserted = command.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        command.CommandText = "SELECT TOP 1 SocietyID FROM Societies WHERE SocietyName = @SocietyName AND CreatedByStudentID = @CreatedByStudentID ORDER BY SocietyID DESC";
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public int CreateSocietyMember(SocietyMember societyMember)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM SocietyMembers", connection))
                {
                    int societyMemberCount = (int)command.ExecuteScalar();
                    societyMemberCount++; // Increment the count to get the new SocietyMemberID

                    command.CommandText = "INSERT INTO SocietyMembers (SocietyMemberID, StudentID, SocietyID, TeamName, JoinedDate) VALUES (@SocietyMemberID, @StudentID, @SocietyID, @TeamName, GETDATE())";
                    command.Parameters.AddWithValue("@SocietyMemberID", societyMemberCount);
                    command.Parameters.AddWithValue("@StudentID", societyMember.StudentID);
                    command.Parameters.AddWithValue("@SocietyID", societyMember.SocietyID);
                    command.Parameters.AddWithValue("@TeamName", societyMember.TeamName);
                    int rowsInserted = command.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        command.CommandText = "SELECT TOP 1 SocietyMemberID FROM SocietyMembers WHERE StudentID = @StudentID AND SocietyID = @SocietyID ORDER BY SocietyMemberID DESC";
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public int CreateSocietyExecutive(SocietyExecutive societyExecutive)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM SocietyExecutives", connection))
                {
                    int executiveCount = (int)command.ExecuteScalar();
                    executiveCount++; // Increment the count to get the new ExecutiveID

                    command.CommandText = "INSERT INTO SocietyExecutives (ExecutiveID, SocietyID, StudentID, Position, SocietyMemberID) VALUES (@ExecutiveID, @SocietyID, @StudentID, 'President', @SocietyMemberID)";
                    command.Parameters.AddWithValue("@ExecutiveID", executiveCount);
                    command.Parameters.AddWithValue("@SocietyID", societyExecutive.SocietyID);
                    command.Parameters.AddWithValue("@StudentID", societyExecutive.StudentID);
                    command.Parameters.AddWithValue("@SocietyMemberID", societyExecutive.SocietyMemberID);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool DeleteSociety(int societyID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Start a new transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete the society executives
                        using (SqlCommand command = new SqlCommand("DELETE FROM SocietyExecutives WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

                        // Delete the society members
                        using (SqlCommand command = new SqlCommand("DELETE FROM SocietyMembers WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

                        // Delete the society
                        using (SqlCommand command = new SqlCommand("DELETE FROM Societies WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction
                        transaction.Rollback();

                        return false;
                    }
                }
            }
        }

        public int CreateMembershipRequest(MembershipRequest request)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // first get the number of number of rows in the table SocietyCreationApplications
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM SocietyCreationApplications", connection))
                {
                    int count = (int)command1.ExecuteScalar();
                    count++;


                    // Insert the request into the database.
                    using (SqlCommand command = new SqlCommand("INSERT INTO MembershipRequests (RequestID, StudentID, SocietyID, RequestDate, Status, DepartmentName, TeamName, Description) VALUES (@RequestID, @StudentID, @SocietyID, @RequestDate, @Status, @DepartmentName, @TeamName, @Description)", connection))
                    {
                        command.Parameters.AddWithValue(@"RequestID", count);
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@SocietyID", request.SocietyID);
                        command.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@TeamName", request.TeamName);
                        command.Parameters.AddWithValue("@Description", request.Description);

                        int rowsAffected = command.ExecuteNonQuery();


                        if (rowsAffected > 0)
                        {
                            return count; // return the ApplicationID of the newly created application
                        }
                        else
                        {
                            return -1; // return -1 to indicate that the insertion failed
                        }
                    }
                }
            }
        }
        public int UpdateMembershipStatus(int requestID, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE MembershipRequests SET Status = @Status WHERE RequestID = @RequestID", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@RequestID", requestID);
                    return command.ExecuteNonQuery();
                }
            }
        }
        public MembershipRequest GetMembershipRequestDetails(int requestID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MembershipRequests WHERE RequestID = @RequestID", connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MembershipRequest
                            {
                                RequestID = reader.GetInt32(reader.GetOrdinal("RequestID")),
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                SocietyID = reader.GetInt32(reader.GetOrdinal("SocietyID")),
                                TeamName = reader.GetString(reader.GetOrdinal("TeamName")),
                                RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                                Description = reader.GetString(reader.GetOrdinal("Description"))
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

    }


}
