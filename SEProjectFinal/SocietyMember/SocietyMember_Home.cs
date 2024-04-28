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

        

        private Student currentStudent;
        private SocietyMember SocietyMember;
        public SocietyMember_Home(Student student)
        {
            InitializeComponent();

            dataGridView1.Visible = false;
            label2.Visible = false;
            currentStudent = student;
            this.Text = "Society Member Home";
            SocietyService societyService = new SocietyService(our_connection_string);
            SocietyMember = societyService.GetSocietyMemberByStudentId(student.StudentID);
        }

        

        private void SocietyMember_Home_Load(object sender, EventArgs e)
        {
            bunifuLabel1.Text="Welcome Society Member " + currentStudent.FullName  ;
        }

        



        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateSociety createSociety = new CreateSociety(this, this.currentStudent);
            createSociety.Show();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
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

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            // View all societies ( we display them )
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAllSocieties();

            dataGridView1.DataSource = dataTable;
            dataGridView1.Visible = true;
            label2.Text = "Viewing All Societies";
            label2.Visible = true;
        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            this.Hide();
            JoinSociety joinSociety = new JoinSociety(this, this.currentStudent);
            joinSociety.Show();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            // view your joined societies announcements
            // your joined societies can be multiple
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAnnouncementsForJoinedSocieties(currentStudent.StudentID);

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            // Show the DataGridView
            dataGridView1.Visible = true;
            label2.Text = "Viewing Announcements From Societies You Have Joined";
            label2.Visible = true;
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            // view your joined societies events
            // your joined societies can be multiple
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEventsForJoinedSocieties(currentStudent.StudentID);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Visible = true;
            label2.Text = "Viewing Events From Societies You Have Joined";
            label2.Visible = true;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }
    }
}
