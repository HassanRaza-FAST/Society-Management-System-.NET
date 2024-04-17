using SEProjectFinal.DomainModel;
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

namespace SEProjectFinal
{
    public partial class SocietyExecutive_Home : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        private Student student;
        private SocietyExecutive societyExecutive;
       

        public SocietyExecutive_Home(Student student)
        {
            InitializeComponent();
            this.Text = "Society Executive Home";
            label2.Visible = false;
            dataGridView1.Visible = false;
            this.student = student;
            SocietyService societyService = new SocietyService(our_connection_string);
            societyExecutive = societyService.GetSocietyExecutiveByStudentId(student.StudentID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SocietyExecutive_Home_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEventsByStatus("Pending", societyExecutive.SocietyID);

            dataGridView1.DataSource = dataTable;
            label2.Text = "Viewing all your Event Requests";
            label2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEvents(societyExecutive.SocietyID);

            dataGridView1.DataSource = dataTable;
            label2.Text = "Viewing all your Schedued Events";
            label2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CreateEvent createEvent = new CreateEvent(this);
            createEvent.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CreateAnnouncement createAnnouncement = new CreateAnnouncement(this);
            createAnnouncement.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAnnouncementofExec(societyExecutive.StudentID);

            dataGridView1.DataSource = dataTable;
            label2.Text = "Viewing Announcements made by you";
            label2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //a society exec may be member of other societies as well
            // show all events of all societies that this society exec is member of
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEventsForJoinedSocieties(student.StudentID);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Visible = true;
            label1.Text = "Viewing Events From Societies You Have Joined";
            label1.Visible = true;

        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAnnouncementsForJoinedSocieties(student.StudentID);

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            // Show the DataGridView
            dataGridView1.Visible = true;
            label1.Text = "Viewing Announcements From Societies You Have Joined";
            label1.Visible = true;
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Edit_Announcement edit_Announcement = new Edit_Announcement(this, societyExecutive);
            edit_Announcement.Show();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Edit_Event edit_Event = new Edit_Event(this, societyExecutive);
            edit_Event.Show();
        }
    }
}
