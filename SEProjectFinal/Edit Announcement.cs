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

namespace SEProjectFinal
{
    public partial class Edit_Announcement : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        SocietyExecutive_Home societyExecutive_Home;
        SocietyExecutive societyExecutive;
        public Edit_Announcement(SocietyExecutive_Home societyExecutive_Home, SocietyExecutive societyExecutive)
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            this.societyExecutive_Home = societyExecutive_Home;
            this.societyExecutive = societyExecutive;
            this.Load += Edit_Announcement_Load;
        }

        private void Edit_Announcement_Load(object sender, EventArgs e)
        {
            // show the announcements in grid
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAnnouncementofExec(societyExecutive.StudentID);
            dataGridView1.DataSource = dataTable;
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        //make a function of data grid that will show the data of the selected row in the textboxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Title"].Value.ToString();
                richTextBox1.Text = row.Cells["Description"].Value.ToString();
            }
        }
       



        private void returnbtn_Click_1(object sender, EventArgs e)
        {
            
        }

        private void exitbtn_Click_1(object sender, EventArgs e)
        {
            //close app
            
        }

        private void logoutbtn_Click_1(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            // after the user has clicked on the row, the data will be shown in the textboxes
            // now he can edit the data and click on the edit button
            // the data will be updated in the database
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int announcementID = (int)dataGridView1.SelectedRows[0].Cells["AnnouncementID"].Value;
                string title = textBox1.Text;
                string description = richTextBox1.Text;

                SocietyService societyService = new SocietyService(our_connection_string);
                bool isUpdated = societyService.UpdateAnnouncement(announcementID, title, description);

                if (isUpdated)
                {
                    MessageBox.Show("Announcement updated successfully.");
                    Edit_Announcement_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("An error occurred while updating the announcement.");
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            // return to previous page
            this.Hide();
            societyExecutive_Home.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
