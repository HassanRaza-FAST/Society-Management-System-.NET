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
    public partial class Student_Home : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        public Student_Home()
        {
            InitializeComponent();
            Viewing_Socities_Grid.Visible = false;
            Viewing_societies_label.Visible = false;
            this.Text = "Student Home";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // View all societies ( we display them )
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAllSocieties();

            Viewing_Socities_Grid.DataSource = dataTable;
            Viewing_Socities_Grid.Visible = true;
            Viewing_societies_label.Text = "Viewing All Societies";
            Viewing_societies_label.Visible = true;
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void createSocietylbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CreateSociety createSociety = new CreateSociety(this);
            createSociety.Show();
        }
    }
}
