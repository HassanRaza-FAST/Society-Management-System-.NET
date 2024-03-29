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
using SEProjectFinal.BLL;

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
            UserService userService = new UserService(our_connection_string);
            bool areCredentialsValid = userService.AreCredentialsValid(textBox1.Text, textBox2.Text, "Admin");

            if (areCredentialsValid)
            {
                // Log the user in
                this.Hide();
                Admin_Home admin_Home = new Admin_Home();
                admin_Home.Show();
            }
            else
            {
                // Show an error message
                MessageBox.Show("Invalid Credentials");
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
