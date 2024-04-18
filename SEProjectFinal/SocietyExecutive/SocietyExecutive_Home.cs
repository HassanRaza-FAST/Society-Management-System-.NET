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

        


        private void SocietyExecutive_Home_Load(object sender, EventArgs e)
        {

        }

        

        

        

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateEvent createEvent = new CreateEvent(this);
            createEvent.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEventsByStatus("Pending", societyExecutive.SocietyID);

            dataGridView1.DataSource = dataTable;
            label2.Text = "Viewing your Event Requests";
            label2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetEvents(societyExecutive.SocietyID);

            dataGridView1.DataSource = dataTable;
            label2.Text = "Viewing all Schedued Events";
            label2.Visible = true;
            dataGridView1.Visible = true;
        }
    }
}
