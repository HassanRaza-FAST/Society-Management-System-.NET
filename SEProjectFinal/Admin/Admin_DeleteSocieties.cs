

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

namespace SEProjectFinal.Admin
{
    public partial class Admin_DeleteSocieties : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public Admin_DeleteSocieties()
        {
            InitializeComponent();
            this.Text = "Delete Societies";
        }


        private void Admin_DeleteSocieties_Load(object sender, EventArgs e)
        {
            // Create a SocietyService instance
            SocietyService societyService = new SocietyService(our_connection_string);

            // Use the SocietyService to get all societies
            dataGridView1.DataSource = societyService.GetAllSocieties();
        }


       
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if a society is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int societyID = (int)dataGridView1.SelectedRows[0].Cells["SocietyID"].Value;

                // Create a SocietyService instance
                SocietyService societyService = new SocietyService(our_connection_string);

                // Delete the society
                bool isDeleted = societyService.DeleteSociety(societyID);

                if (isDeleted)
                {
                    MessageBox.Show("Society deleted successfully.");

                    // Refresh the DataGridView
                    Admin_DeleteSocieties_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("An error occurred while deleting the society.");
                }
            }
            else
            {
                MessageBox.Show("Please select a society to delete.");
            }
        }


        private void returnbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Home admin_Home = new Admin_Home();
            admin_Home.Show();
        }
    }
}

