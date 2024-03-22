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
    public partial class AdminLogin : Form
    {
        private string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        public AdminLogin()
        {
            InitializeComponent();
            this.Text = "Admin Login";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Administrators WHERE Email = @Email AND Password = @Password";
            using (SqlConnection connection = new SqlConnection(our_connection_string))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@Password", textBox2.Text.Trim());
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                this.Hide();
                                Admin_Home admin_home = new Admin_Home();
                                admin_home.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password");
                            }
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }
    }
}
