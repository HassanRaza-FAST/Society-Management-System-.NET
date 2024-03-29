using SEProjectFinal.BLL;
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
    public partial class MentorLogin : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public MentorLogin()
        {
            InitializeComponent();
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserService userService = new UserService(our_connection_string);
            bool areCredentialsValid = userService.AreCredentialsValid(textBox1.Text, textBox2.Text, "Mentor");

            if (areCredentialsValid)
            {
                // Log the user in
                this.Hide();
                // get Mentor object
                DomainModel.Mentor mentor = userService.GetMentor(textBox1.Text);
                Mentor_Home mentor_Home = new Mentor_Home(mentor);
                mentor_Home.Show();
            }
            else
            {
                // Show an error message
                MessageBox.Show("Invalid Credentials");
            }
        }

        private void MentorLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
