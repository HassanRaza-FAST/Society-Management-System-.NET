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
    public partial class Edit_Event : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        SocietyExecutive_Home societyExecutive_Home;
        SocietyExecutive societyExecutive;
        public Edit_Event(SocietyExecutive_Home societyExecutive_Home, SocietyExecutive societyExecutive)
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            this.societyExecutive_Home = societyExecutive_Home;
            this.societyExecutive = societyExecutive;
            this.Load += Edit_Event_Load;
        }

        private void Edit_Event_Load(object sender, EventArgs e)
        {
            // show the event in grid
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEventsByStatus("Pending", societyExecutive.SocietyID);

            dataGridView1.DataSource = dataTable;
           
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            // after the user has clicked on the row, the data will be shown in the textboxes
            // now he can edit the data and click on the edit button
            // the data will be updated in the database
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int eventID = (int)dataGridView1.SelectedRows[0].Cells["EventID"].Value;
                string eventName = textBox1.Text;
                string location = textBox2.Text;
                string description = richTextBox1.Text;
                DateTime eventDate = dateTimePicker1.Value;

                SocietyService societyService = new SocietyService(our_connection_string);
                bool isUpdated = societyService.UpdateEvent(eventID, eventName, location, description, eventDate);

                if (isUpdated)
                {
                    MessageBox.Show("Event updated successfully.");
                    Edit_Event_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("An error occurred while updating the event.");
                }
            }
            
        }

        //make a function of data grid that will show the data of the selected row in the textboxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["EventName"].Value.ToString();
                textBox2.Text = row.Cells["Location"].Value.ToString();
                richTextBox1.Text = row.Cells["Description"].Value.ToString();
                //convert only when not null
                if (row.Cells["EventDate"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["EventDate"].Value);
                //else make it null
                else
                    dateTimePicker1.Value = DateTime.Now;
            }
        }




        private void returnbtn_Click(object sender, EventArgs e)
        {
            // return to previous page
            this.Hide();
            societyExecutive_Home.Show();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            //close app
            Application.Exit();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentLogin studentLogin = new StudentLogin();
            studentLogin.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Edit_Event_Load_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
