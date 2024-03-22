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
using Microsoft.Data.SqlClient;

namespace SEProjectFinal
{
    public partial class HomeScreen : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public HomeScreen()
        {
            InitializeComponent();
            this.Text = "FastHub - Fast University Society Management System";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            AdminLogin form2 = new AdminLogin();
            form2.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("../../Images/logo2.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //close the application
            Application.Exit();
        }
    }
}
