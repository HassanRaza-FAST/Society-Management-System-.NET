using Azure.Core;
using SEProjectFinal.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProjectFinal.Mentor
{
    public partial class Mentor_ApproveMember : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        private Mentor_Home mentor_home;
        public Mentor_ApproveMember(Mentor_Home mentor_home)
        {
            InitializeComponent();
            this.Load += Mentor_ApproveMember_Load;
            this.mentor_home = mentor_home;
            //this.btnAccept.Click += btnAccept_Click;
            //this.btnReject.Click += btnReject_Click;
        }

        private void Mentor_ApproveMember_Load(object sender, EventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable table = societyService.GetAllMembershipApplications();
            dataGridView1.DataSource = table;
        }
        
        private void UpdateMembershipStatus(int requestID, string status)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            int rowsAffected = societyService.UpdateMembershipStatus(requestID, status);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Membership status updated successfully.");

                if (status == "Accepted")
                {
                    MembershipRequest request = societyService.GetMembershipRequestDetails(requestID);
                    if (request != null)
                    {
                        SocietyMember societyMember = new SocietyMember
                        {
                            StudentID = request.StudentID,
                            SocietyID = request.SocietyID,
                            TeamName = request.TeamName,
                            JoinedDate = DateTime.Now
                        };
                        int societyMemberId = societyService.CreateSocietyMember(societyMember);
                        if (societyMemberId > 0)
                        {
                            MessageBox.Show("Society member created successfully.");
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while creating the society member.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No membership request found with the provided ID.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No membership request found with the provided ID.");
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int requestID = (int)dataGridView1.SelectedRows[0].Cells["RequestID"].Value;
                UpdateMembershipStatus(requestID, "Rejected");
                //show the updated datagrid
                Mentor_ApproveMember_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to reject.");
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int requestID = (int)dataGridView1.SelectedRows[0].Cells["RequestID"].Value;
                UpdateMembershipStatus(requestID, "Accepted");
                //show the updated datagrid
                Mentor_ApproveMember_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to accept.");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorLogin mentorLogin = new MentorLogin();
            mentorLogin.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            //close app
            Application.Exit();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            mentor_home.Show();
        }
    }
}
