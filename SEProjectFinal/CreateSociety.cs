using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace SEProjectFinal
{
    public partial class CreateSociety : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        
        private SocietyMember_Home societyMemberHome;
        private Student_Home studentHome;

        public CreateSociety(SocietyMember_Home societyMemberHome)
        {
            InitializeComponent();
            this.Text = "Create Society";
            this.societyMemberHome = societyMemberHome;
            
        }

        public CreateSociety(Student_Home studentHome)
        {
            InitializeComponent();
            this.Text = "Create Society";
            this.studentHome = studentHome;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check if all text fields are filled in our form
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            // submit the form, show message that it has been submitted and clear all the entries, also make insertion in the database
            int studentID = int.Parse(textBox1.Text);
            string societyName = textBox2.Text;
            string Department = textBox3.Text;
            string Description = richTextBox1.Text;

            using (SqlConnection connection = new SqlConnection(our_connection_string))
            {
                connection.Open();

                // first get the number of number of rows in the table SocietyCreationApplications
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM SocietyCreationApplications", connection))
                {
                    int count = (int)command1.ExecuteScalar();
                    count++;
                    // insert the form into the database
                    using (SqlCommand command2 = new SqlCommand("INSERT INTO SocietyCreationApplications (ApplicationID, StudentID, SocietyName, DepartmentName, Description) VALUES (@ApplicationID, @StudentID, @SocietyName, @DepartmentName, @Description)", connection))
                    {
                        command2.Parameters.AddWithValue("@ApplicationID", count);
                        command2.Parameters.AddWithValue("@StudentID", studentID);
                        command2.Parameters.AddWithValue("@SocietyName", societyName);
                        command2.Parameters.AddWithValue("@DepartmentName", Department);
                        command2.Parameters.AddWithValue("@Description", Description);

                        int rowsAffected = command2.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Society Form has been submitted successfully");
                        }
                        else
                        {
                            MessageBox.Show("There was a problem with the database. Please try again later.");
                        }
                    }
                }
            }


            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            richTextBox1.Text = "";
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (societyMemberHome != null)
            {
                societyMemberHome.Show();
            }
            else
            {
                studentHome.Show();
            }
            
        }
    }
}
