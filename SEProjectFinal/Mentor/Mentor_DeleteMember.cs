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
    public partial class Mentor_DeleteMember : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        Mentor_Home mentorHome;
        SEProjectFinal.DomainModel.Mentor mentor;
        public Mentor_DeleteMember(Mentor_Home mentorHome, DomainModel.Mentor mentor)
        {
            InitializeComponent();
            this.Text = "Delete Members";
            this.mentorHome = mentorHome;
            this.mentor = mentor;
        }

        private void Mentor_DeleteMember_Load(object sender, EventArgs e)
        {
            // Create a SocietyService instance
            SocietyService societyService = new SocietyService(our_connection_string);

            // Use the SocietyService to get all societies
            dataGridView1.DataSource = societyService.GetAllSocietyMembers(mentor.SocietyID);
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            mentorHome.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            // Check if a member is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int studentID = (int)dataGridView1.SelectedRows[0].Cells["StudentID"].Value;
                int societyID = (int)dataGridView1.SelectedRows[0].Cells["SocietyID"].Value;

                // Create a SocietyService instance
                SocietyService societyService = new SocietyService(our_connection_string);

                // Delete the society
                bool isDeleted = societyService.DeleteSocietyMember(studentID, societyID);

                if (isDeleted)
                {
                    MessageBox.Show("Society Member deleted successfully.");

                    // Refresh the DataGridView
                    Mentor_DeleteMember_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("An error occurred while deleting the society Member. Make sure you are not trying to delete Society Executive");
                }
            }
            else
            {
                MessageBox.Show("Please select a society Member to delete.");
            }
        }
    }
}
