using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using SEProjectFinal.DomainModel;
using System.Net.Sockets;
using Microsoft.VisualBasic;

namespace SEProjectFinal
{
    public partial class Admin_SocietyApplications : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public Admin_SocietyApplications()
        {
            InitializeComponent();
            this.Load += Admin_SocietyApplications_Load;
            //this.btnAccept.Click += btnAccept_Click;
            //this.btnReject.Click += btnReject_Click;
        }

        private void Admin_SocietyApplications_Load(object sender, EventArgs e)
        {
            // Load data from database into DataGridView
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable table = societyService.GetAllSocietyApplications();
            dataGridView1.DataSource = table;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int applicationID = (int)dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value;
                UpdateApplicationStatus(applicationID, "Accepted");
                //show the updated datagrid
                Admin_SocietyApplications_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to accept.");
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int applicationID = (int)dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value;
                UpdateApplicationStatus(applicationID, "Rejected");
                //show the updated datagrid
                Admin_SocietyApplications_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to reject.");
            }
        }



        // Method to update application status
        //private void UpdateApplicationStatus(int applicationID, string status)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(our_connection_string))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand("UPDATE SocietyCreationApplications SET Status = @Status WHERE ApplicationID = @ApplicationID", connection))
        //            {
        //                command.Parameters.AddWithValue("@Status", status);
        //                command.Parameters.AddWithValue("@ApplicationID", applicationID);
        //                int rowsAffected = command.ExecuteNonQuery();
        //                if (rowsAffected > 0)
        //                {
        //                    MessageBox.Show("Application status updated successfully.");

        //                    if (status == "Accepted")
        //                    {
        //                        string societyName, description, departmentName;
        //                        int studentId;

        //                        // Clear the parameters
        //                        command.Parameters.Clear();

        //                        // Get the application details
        //                        command.CommandText = "SELECT * FROM SocietyCreationApplications WHERE ApplicationID = @ApplicationID";
        //                        command.Parameters.AddWithValue("@ApplicationID", applicationID);
        //                        using (SqlDataReader reader = command.ExecuteReader())
        //                        {
        //                            if (reader.HasRows)
        //                            {
        //                                reader.Read();
        //                                societyName = reader.GetString(2);
        //                                description = reader.GetString(3);
        //                                studentId = reader.GetInt32(1);
        //                                departmentName = reader.GetString(6);
        //                            }
        //                            else
        //                            {
        //                                MessageBox.Show("No application found with the provided ID.");
        //                                return;
        //                            }
        //                        }

        //                        // Clear the parameters
        //                        command.Parameters.Clear();

        //                        // Insert into Societies table after counting the current rows in the table and increment this value to insert the new SocietyID
        //                        command.CommandText = "SELECT COUNT(*) FROM Societies";
        //                        int societyCount = (int)command.ExecuteScalar();
        //                        societyCount++; // Increment the count to get the new SocietyID

        //                        command.CommandText = "INSERT INTO Societies (SocietyID, SocietyName, Description, CreatedByStudentID, DepartmentName) VALUES (@SocietyID, @SocietyName, @Description, @CreatedByStudentID, @DepartmentName)";
        //                        command.Parameters.AddWithValue("@SocietyID", societyCount);
        //                        command.Parameters.AddWithValue("@SocietyName", societyName);
        //                        command.Parameters.AddWithValue("@Description", description);
        //                        command.Parameters.AddWithValue("@CreatedByStudentID", studentId);
        //                        command.Parameters.AddWithValue("@DepartmentName", departmentName);

        //                        int rowsInserted = command.ExecuteNonQuery();

        //                        if (rowsInserted > 0)
        //                        {
        //                            MessageBox.Show("Society created successfully.");

        //                            // Clear the parameters
        //                            command.Parameters.Clear();

        //                            // Get the SocietyID of the newly created society
        //                            command.CommandText = "SELECT SocietyID FROM Societies WHERE SocietyName = @SocietyName AND CreatedByStudentID = @CreatedByStudentID";
        //                            command.Parameters.AddWithValue("@SocietyName", societyName);
        //                            command.Parameters.AddWithValue("@CreatedByStudentID", studentId);
        //                            int societyId = (int)command.ExecuteScalar();

        //                            // Clear the parameters
        //                            command.Parameters.Clear();

        //                            //Insert into SocietyMembers table after counting number of rows in the table and increment this value to insert the new SocietyMemberID
        //                            command.CommandText = "SELECT COUNT(*) FROM SocietyMembers";
        //                            int societyMemberCount = (int)command.ExecuteScalar();
        //                            societyMemberCount++; // Increment the count to get the new SocietyMemberID

        //                            // Insert into SocietyMembers table
        //                            command.CommandText = "INSERT INTO SocietyMembers (SocietyMemberID, StudentID, SocietyID, JoinedDate) VALUES (@SocietyMemberID, @StudentID, @SocietyID, GETDATE())";
        //                            command.Parameters.AddWithValue("@StudentID", studentId);
        //                            command.Parameters.AddWithValue("@SocietyID", societyId);
        //                            command.Parameters.AddWithValue("@SocietyMemberID", societyMemberCount);

        //                            //if insertion into society member was done then proceed on inserting into society executives
        //                            rowsInserted = command.ExecuteNonQuery();

        //                            if (rowsInserted > 0)
        //                            {
        //                                // Clear the parameters
        //                                command.Parameters.Clear();

        //                                // Get the SocietyMemberID of the newly created society member
        //                                command.CommandText = "SELECT SocietyMemberID FROM SocietyMembers WHERE StudentID = @StudentID AND SocietyID = @SocietyID";
        //                                command.Parameters.AddWithValue("@StudentID", studentId);
        //                                command.Parameters.AddWithValue("@SocietyID", societyId);
        //                                int societyMemberId = (int)command.ExecuteScalar();

        //                                // Clear the parameters
        //                                command.Parameters.Clear();

        //                                //Insert into Executives table after counting number of rows in the table and increment this value to insert the new ExecutiveID
        //                                command.CommandText = "SELECT COUNT(*) FROM SocietyExecutives";
        //                                int executiveCount = (int)command.ExecuteScalar();
        //                                executiveCount++; // Increment the count to get the new ExecutiveID

        //                                // Insert into SocietyExecutives table
        //                                command.CommandText = "INSERT INTO SocietyExecutives (ExecutiveID, SocietyID, StudentID, Position, SocietyMemberID) VALUES (@ExecutiveID, @SocietyID, @StudentID, 'President', @SocietyMemberID)";
        //                                command.Parameters.AddWithValue("@SocietyMemberID", societyMemberId);
        //                                command.Parameters.AddWithValue("@SocietyID", societyId);
        //                                command.Parameters.AddWithValue("@ExecutiveID", executiveCount);
        //                                command.Parameters.AddWithValue("@StudentID", studentId);
        //                                command.ExecuteNonQuery();

        //                                MessageBox.Show("The applicant has been assigned as the President.");
        //                            }
        //                            else
        //                            {
        //                                MessageBox.Show("An error occurred while creating the Society Member table.");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("An error occurred while creating the society.");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("No application found with the provided ID.");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred while updating the application status: " + ex.Message);
        //    }
        //}

        private void UpdateApplicationStatus(int applicationID, string status)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            int rowsAffected = societyService.UpdateApplicationStatus(applicationID, status);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Application status updated successfully.");

                if (status == "Accepted")
                {
                    SocietyApplication application = societyService.GetApplicationDetails(applicationID);
                    if (application != null)
                    {
                        Society society = new Society
                        {
                            SocietyName = application.SocietyName,
                            Description = application.Description,
                            CreatedByStudentID = application.StudentID,
                            DepartmentName = application.DepartmentName
                        };

                        int mentorId = PromptForMentorId();

                        int societyId = societyService.CreateSociety(society, mentorId);
                        if (societyId > 0)
                        {
                            MessageBox.Show("Society created successfully.");

                            SocietyMember societyMember = new SocietyMember
                            {
                                StudentID = application.StudentID,
                                SocietyID = societyId,
                                JoinedDate = DateTime.Now
                            };
                            int societyMemberId = societyService.CreateSocietyMember(societyMember);
                            if (societyMemberId > 0)
                            {
                                MessageBox.Show("Society member created successfully.");

                                SocietyExecutive societyExecutive = new SocietyExecutive
                                {
                                    SocietyID = societyId,
                                    StudentID = application.StudentID,
                                    Position = "President",
                                    SocietyMemberID = societyMemberId
                                };
                                if(societyService.CreateSocietyExecutive(societyExecutive)>0)
                                    MessageBox.Show("The applicant has been assigned as the President.");
                                else
                                    MessageBox.Show("An error occurred while creating the Society Executive table.");
                            }
                            else
                            {
                                MessageBox.Show("An error occurred while creating the Society Member table.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while creating the society.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No application found with the provided ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No application found with the provided ID.");
            }
        }



        

        

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
        }

        private void btnAccept_Click_1(object sender, EventArgs e)
        {

        }

        private void btnReject_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            // return to previous page
            this.Hide();
            Admin_Home admin_Home = new Admin_Home();
            admin_Home.Show();
        }


        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int applicationID = (int)dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value;
                UpdateApplicationStatus(applicationID, "Rejected");
                //show the updated datagrid
                Admin_SocietyApplications_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to reject.");
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int applicationID = (int)dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value;
                UpdateApplicationStatus(applicationID, "Accepted");
                //show the updated datagrid
                Admin_SocietyApplications_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to accept.");
            }
        }

        private int PromptForMentorId()
        {
            int mentorId = 0;
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the mentor ID for the newly created society:", "Enter Mentor ID", "");
            if (!string.IsNullOrEmpty(input))
            {
                if (int.TryParse(input, out mentorId))
                {
                    // Mentor ID entered successfully
                    return mentorId;
                }
                else
                {
                    MessageBox.Show("Invalid mentor ID. Please enter a valid numeric ID.");
                }
            }
            else
            {
                MessageBox.Show("No mentor ID entered. Please enter a valid numeric ID.");
            }

            // Return 0 if mentor ID was not entered or invalid
            return mentorId;
        }



    }
}
