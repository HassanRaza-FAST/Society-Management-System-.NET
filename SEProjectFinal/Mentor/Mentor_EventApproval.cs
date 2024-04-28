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
    public partial class Mentor_EventApproval : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        private Mentor_Home mentor_Home;

        // need Mentor from Domain Model
        DomainModel.Mentor mentor;



        public Mentor_EventApproval(Mentor_Home mentor_Home, DomainModel.Mentor mentor)
        {
            InitializeComponent();
            this.mentor_Home = mentor_Home;
            this.mentor = mentor;
            this.Load += Mentor_EventApproval_Load;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int eventID = (int)dataGridView1.SelectedRows[0].Cells["EventID"].Value;
                UpdateEventStatus(eventID, "Accepted");
                //show the updated datagrid
                Mentor_EventApproval_Load(sender, e);
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
                int eventID = (int)dataGridView1.SelectedRows[0].Cells["EventID"].Value;
                UpdateEventStatus(eventID, "Rejected");
                //show the updated datagrid
                Mentor_EventApproval_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to reject.");
            }
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorLogin mentorLogin = new MentorLogin();
            mentorLogin.Show();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            // return to prev page
            this.Hide();
            mentor_Home.Show();
        }

        private void Mentor_EventApproval_Load(object sender, EventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable table = societyService.GetEventsByStatus("Pending", mentor.SocietyID);
            dataGridView1.DataSource = table;
        }
        private void UpdateEventStatus(int eventId, string status)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            societyService.UpdateEventStatus(eventId, status);
            MessageBox.Show("Event status updated successfully.");
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int eventID = (int)dataGridView1.SelectedRows[0].Cells["EventID"].Value;
                UpdateEventStatus(eventID, "Rejected");
                //show the updated datagrid
                Mentor_EventApproval_Load(sender, e);
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
                int eventID = (int)dataGridView1.SelectedRows[0].Cells["EventID"].Value;
                UpdateEventStatus(eventID, "Accepted");
                //show the updated datagrid
                Mentor_EventApproval_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an application to accept.");
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            // return to prev page
            this.Hide();
            mentor_Home.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorLogin mentorLogin = new MentorLogin();
            mentorLogin.Show();
        }
    }
}
