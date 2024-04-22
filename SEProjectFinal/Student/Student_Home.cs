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
        private Student student;
        public Student_Home(Student student)
        {
            InitializeComponent();
            Viewing_Socities_Grid.Visible = false;
            Viewing_societies_label.Visible = false;
            this.Text = "Student Home";
            this.student = student;
        }




        private void Student_Home_Load(object sender, EventArgs e)
        {
            bunifuLabel1.Text = "Welcome " + student.FullName;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentLogin form3 = new StudentLogin();
            form3.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            // View all societies ( we display them )
            SocietyService societyService = new SocietyService(our_connection_string);
            DataTable dataTable = societyService.GetAllSocieties();

            Viewing_Socities_Grid.DataSource = dataTable;
            Viewing_Socities_Grid.Visible = true;
            Viewing_societies_label.Text = "Viewing All Societies";
            Viewing_societies_label.Visible = true;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateSociety createSociety = new CreateSociety(this, this.student);
            createSociety.Show();
        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            this.Hide();
            JoinSociety joinSociety = new JoinSociety(this);
            joinSociety.Show();
        }
    }
}
