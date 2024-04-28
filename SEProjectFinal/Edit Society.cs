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
    public partial class Edit_Society : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public Edit_Society()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            this.Load += Edit_Society_Load;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["SocietyName"].Value.ToString();
                textBox2.Text = row.Cells["MentorName"].Value.ToString();
                richTextBox1.Text = row.Cells["Description"].Value.ToString();
               
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }


        private void exitbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void Edit_Society_Load(object sender, EventArgs e)
        {
            // view all societies
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dt = societyService.GetAllSocieties();
            dataGridView1.DataSource = dt;
            dataGridView1.Visible = true;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Home admin_Home = new Admin_Home();
            admin_Home.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the details from the textboxes
                int societyID = (int)dataGridView1.SelectedRows[0].Cells["SocietyID"].Value;
                string societyName = textBox1.Text;
                string mentorName = textBox2.Text;
                string description = richTextBox1.Text;

                // Create a new instance of SocietyService
                SocietyService societyService = new SocietyService(our_connection_string);

                // Update the society in the database
                bool isUpdated = societyService.UpdateSociety(societyID, societyName, mentorName, description);

                if (isUpdated)
                {
                    MessageBox.Show("Society updated successfully.");
                }
                else
                {
                    MessageBox.Show("Error updating the society.");
                }

                // Refresh the dataGridView1
                DataTable dataTable = societyService.GetAllSocieties();
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
