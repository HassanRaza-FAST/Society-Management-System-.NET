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

namespace SEProjectFinal
{
    public partial class SocietyMember_Home : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // added to remove error
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // added to remove error
        }

        private Student currentStudent;

        public SocietyMember_Home(Student student)
        {
            InitializeComponent();

            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            label2.Visible = false;
            currentStudent = student;
            this.Text = "Society Member Home";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // View all societies ( we display them )
            using (SqlConnection connection = new SqlConnection(our_connection_string))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Societies", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Show the DataGridView
                        dataGridView1.Visible = true;
                        label2.Text = "Viewing All Societies";
                        label2.Visible = true;
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // view all societies that this society member has joined
            using (SqlConnection connection = new SqlConnection(our_connection_string))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Societies WHERE SocietyID IN (SELECT SocietyID FROM SocietyMembers WHERE StudentID = @studentId)", connection))
                {
                    command.Parameters.AddWithValue("@studentId", currentStudent.StudentID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Show the DataGridView
                        dataGridView1.Visible = true;
                        label2.Text = "Viewing Societies You Have Joined";
                        label2.Visible = true;
                    }
                }
            }
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CreateSociety createSociety = new CreateSociety(this);
            createSociety.Show();
        }

        private void SocietyMember_Home_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
