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
using SEProjectFinal.DomainModel;

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
        private SocietyMember SocietyMember;
        public SocietyMember_Home(Student student)
        {
            InitializeComponent();

            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            label2.Visible = false;
            currentStudent = student;
            this.Text = "Society Member Home";
            SocietyService societyService = new SocietyService(our_connection_string);
            SocietyMember = societyService.GetSocietyMemberByStudentId(student.StudentID);
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
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAllSocieties();

            dataGridView1.DataSource = dataTable;
            dataGridView1.Visible = true;
            label2.Text = "Viewing All Societies";
            label2.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // view all societies that this society member has joined
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetSocietiesByStudentId(currentStudent.StudentID);

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            // Show the DataGridView
            dataGridView1.Visible = true;
            label2.Text = "Viewing Societies You Have Joined";
            label2.Visible = true;
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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            JoinSociety joinSociety = new JoinSociety(this);
            joinSociety.Show();
        }
    }
}
