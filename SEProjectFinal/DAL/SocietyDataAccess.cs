using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("SELECT * FROM Societies", connection))
            //    {
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            return dataTable;
            //        }
            //    }
            //}

            // this query should show all societies, along with mentor which has been assigned to that particular society
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT S.SocietyID, S.SocietyName, S.Description, S.CreatedByStudentID, S.DepartmentName, M.FullName as MentorName FROM Societies S LEFT JOIN Mentors M ON S.SocietyID = M.SocietyID", connection))
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

                // insert the form into the database
                using (SqlCommand command = new SqlCommand("INSERT INTO SocietyCreationApplications (StudentID, SocietyName, DepartmentName, Description) VALUES (@StudentID, @SocietyName, @DepartmentName, @Description)", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", application.StudentID);
                    command.Parameters.AddWithValue("@SocietyName", application.SocietyName);
                    command.Parameters.AddWithValue("@DepartmentName", application.DepartmentName);
                    command.Parameters.AddWithValue("@Description", application.Description);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1; // return -1 to indicate that the insertion failed
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

        public int CreateSocietyMember(SocietyMember societyMember)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO SocietyMembers (StudentID, SocietyID, TeamName, JoinedDate) VALUES (@StudentID, @SocietyID, @TeamName, GETDATE())", connection))
                {
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
                using (SqlCommand command = new SqlCommand("INSERT INTO SocietyExecutives (SocietyID, StudentID, Position, SocietyMemberID) VALUES (@SocietyID, @StudentID, 'President', @SocietyMemberID)", connection))
                {
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
                        // Delete Membership Requests associated with this societyID
                        using (SqlCommand command = new SqlCommand("DELETE FROM MembershipRequests WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

                        // Delete from Events
                        using (SqlCommand command = new SqlCommand("DELETE FROM Events WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

                        //Delete from Announcements
                        using (SqlCommand command = new SqlCommand("DELETE FROM Announcements WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

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

                // Insert the request into the database.
                using (SqlCommand command = new SqlCommand("INSERT INTO MembershipRequests (StudentID, SocietyID, RequestDate, Status, DepartmentName, TeamName, Description) VALUES (@StudentID, @SocietyID, @RequestDate, @Status, @DepartmentName, @TeamName, @Description)", connection))
                {
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
                        return 1;
                    }
                    else
                    {
                        return -1; // return -1 to indicate that the insertion failed
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
        public int CreateEvent(Event newEvent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //insert the new event
                using (SqlCommand command = new SqlCommand("INSERT INTO Events (SocietyID, EventName, Description, EventDate, Location, CreatedByStudentID, Status) OUTPUT INSERTED.EventID VALUES (@SocietyID, @EventName, @Description, @EventDate, @Location, @CreatedByStudentID, 'Pending')", connection))
                {
                    command.Parameters.AddWithValue("@SocietyID", newEvent.SocietyID);
                    command.Parameters.AddWithValue("@EventName", newEvent.EventName);
                    command.Parameters.AddWithValue("@Description", newEvent.Description);
                    command.Parameters.AddWithValue("@EventDate", newEvent.EventDate);
                    command.Parameters.AddWithValue("@Location", newEvent.Location);
                    command.Parameters.AddWithValue("@CreatedByStudentID", newEvent.CreatedByStudentID);

                    return (int)command.ExecuteScalar();
                }
            }
        }
        public DataTable GetEventsByStatus(string status, int societyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Status = @Status AND SocietyID = @SocietyID", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@SocietyID", societyId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
        public DataTable GetEvents(int societyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE SocietyID = @SocietyID", connection))
                {
                    command.Parameters.AddWithValue("@SocietyID", societyId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
        public SocietyExecutive GetSocietyExecutiveByStudentId(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyExecutives WHERE StudentID = @StudentID", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SocietyExecutive
                            {
                                ExecutiveID = reader.GetInt32(reader.GetOrdinal("ExecutiveID")),
                                SocietyID = reader.GetInt32(reader.GetOrdinal("SocietyID")),
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                Position = reader.GetString(reader.GetOrdinal("Position")),
                                SocietyMemberID = reader.GetInt32(reader.GetOrdinal("SocietyMemberID"))
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
        public SocietyMember GetSocietyMemberByStudentId(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyMembers WHERE StudentID = @StudentID", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SocietyMember
                            {
                                SocietyMemberID = reader.GetInt32(reader.GetOrdinal("SocietyMemberID")),
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                SocietyID = reader.GetInt32(reader.GetOrdinal("SocietyID")),
                                TeamName = reader.GetString(reader.GetOrdinal("TeamName")),
                                JoinedDate = reader.GetDateTime(reader.GetOrdinal("JoinedDate"))
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
        public void UpdateEventStatus(int eventId, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Events SET Status = @Status WHERE EventID = @EventID", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@EventID", eventId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int CreateAnnouncement(Announcement newAnnouncement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // insert the new announcement
                using (SqlCommand command = new SqlCommand("INSERT INTO Announcements (SocietyID, Title, Description, CreatedByStudentID, CreatedDate) OUTPUT INSERTED.AnnouncementID VALUES (@SocietyID, @Title, @Description, @CreatedByStudentID, @CreatedDate)", connection))
                {
                    command.Parameters.AddWithValue("@SocietyID", newAnnouncement.SocietyID);
                    command.Parameters.AddWithValue("@Title", newAnnouncement.Title);
                    command.Parameters.AddWithValue("@Description", newAnnouncement.Description);
                    command.Parameters.AddWithValue("@CreatedByStudentID", newAnnouncement.CreatedByStudentID);
                    command.Parameters.AddWithValue("@CreatedDate", newAnnouncement.CreatedDate);

                    return (int)command.ExecuteScalar();
                }
            }
        }
        public DataTable GetAnnouncementofExec(int studentId)
        {
            // get all announcements where studentId = createdbystudentid
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Announcements WHERE CreatedByStudentID = @StudentID", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
        public DataTable GetAnnouncementsForJoinedSocieties(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT A.* 
            FROM Announcements A
            INNER JOIN SocietyMembers SM ON A.SocietyID = SM.SocietyID
            WHERE SM.StudentID = @studentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        public DataTable GetEventsForJoinedSocieties(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                        SELECT E.* 
                        FROM Events E
                        INNER JOIN SocietyMembers SM ON E.SocietyID = SM.SocietyID
                        WHERE SM.StudentID = @studentId AND E.Status = 'Accepted'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        public int CreateSociety(Society society, int mentorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Societies (SocietyName, Description, CreatedByStudentID, DepartmentName) VALUES (@SocietyName, @Description, @CreatedByStudentID, @DepartmentName)", connection))
                {
                    command.Parameters.AddWithValue("@SocietyName", society.SocietyName);
                    command.Parameters.AddWithValue("@Description", society.Description);
                    command.Parameters.AddWithValue("@CreatedByStudentID", society.CreatedByStudentID);
                    command.Parameters.AddWithValue("@DepartmentName", society.DepartmentName);
                    int rowsInserted = command.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        // Assign the mentor to the newly created society
                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Mentors SET SocietyID = (SELECT TOP 1 SocietyID FROM Societies WHERE SocietyName = @SocietyName AND CreatedByStudentID = @CreatedByStudentID ORDER BY SocietyID DESC) WHERE MentorID = @MentorID", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@SocietyName", society.SocietyName);
                            updateCommand.Parameters.AddWithValue("@CreatedByStudentID", society.CreatedByStudentID);
                            updateCommand.Parameters.AddWithValue("@MentorID", mentorId);
                            updateCommand.ExecuteNonQuery();
                        }

                        using (SqlCommand selectCommand = new SqlCommand("SELECT TOP 1 SocietyID FROM Societies WHERE SocietyName = @SocietyName AND CreatedByStudentID = @CreatedByStudentID ORDER BY SocietyID DESC", connection))
                        {
                            selectCommand.Parameters.AddWithValue("@SocietyName", society.SocietyName);
                            selectCommand.Parameters.AddWithValue("@CreatedByStudentID", society.CreatedByStudentID);
                            return Convert.ToInt32(selectCommand.ExecuteScalar());
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public DataTable GetAllMentors()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Mentors", connection))
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
        public DataTable GetAllStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Students", connection))
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
        public DataTable GetAllSocietyMembers(int societyID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyMembers WHERE SocietyID = @SocietyID", connection))
                {
                    command.Parameters.AddWithValue("@SocietyID", societyID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }

        }
        public bool DeleteSocietyMember(int studentID, int societyID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM SocietyMembers WHERE StudentID = @StudentID AND SocietyID = @SocietyID", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@SocietyID", societyID);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public SocietyExecutive GetSocietyExecutiveByStudentId(int studentID, int societyID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM SocietyExecutives WHERE StudentID = @StudentID AND SocietyID = @SocietyID", connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@SocietyID", societyID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SocietyExecutive
                            {
                                ExecutiveID = reader.GetInt32(reader.GetOrdinal("ExecutiveID")),
                                SocietyID = reader.GetInt32(reader.GetOrdinal("SocietyID")),
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                Position = reader.GetString(reader.GetOrdinal("Position")),
                                SocietyMemberID = reader.GetInt32(reader.GetOrdinal("SocietyMemberID"))
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
        public bool UpdateAnnouncement(int announcementID, string title, string description)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Announcements SET Title = @Title, Description = @Description WHERE AnnouncementID = @AnnouncementID", connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@AnnouncementID", announcementID);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool UpdateEvent(int eventID, string eventName, string location, string description, DateTime eventDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Events SET EventName = @EventName, Location = @Location, Description = @Description, EventDate = @EventDate WHERE EventID = @EventID", connection))
                {
                    command.Parameters.AddWithValue("@EventName", eventName);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@EventDate", eventDate);
                    command.Parameters.AddWithValue("@EventID", eventID);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }



        public bool UpdateSociety(int societyID, string societyName, string description, int newMentorID, int originalMentorID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Start a new transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update the Societies table
                        using (SqlCommand command = new SqlCommand("UPDATE Societies SET SocietyName = @SocietyName, Description = @Description WHERE SocietyID = @SocietyID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyName", societyName);
                            command.Parameters.AddWithValue("@Description", description);
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.ExecuteNonQuery();
                        }

                        // Set the SocietyID of the original mentor to null
                        using (SqlCommand command = new SqlCommand("UPDATE Mentors SET SocietyID = NULL WHERE MentorID = @OriginalMentorID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@OriginalMentorID", originalMentorID);
                            command.ExecuteNonQuery();
                        }

                        // Update the SocietyID of the new mentor
                        using (SqlCommand command = new SqlCommand("UPDATE Mentors SET SocietyID = @SocietyID WHERE MentorID = @NewMentorID", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SocietyID", societyID);
                            command.Parameters.AddWithValue("@NewMentorID", newMentorID);
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

    }

}



