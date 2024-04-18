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
using SEProjectFinal.Admin;

namespace SEProjectFinal
{
    public partial class Admin_Home : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public Admin_Home()
        {
            InitializeComponent();
            
        }


        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_SocietyApplications admin_society_applications = new Admin_SocietyApplications();
            admin_society_applications.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_DeleteSocieties admin_delete_societies = new Admin_DeleteSocieties();
            admin_delete_societies.Show();
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            // return to homepage
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }
    }
}
