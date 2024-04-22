using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEProjectFinal.Mentor
{
    public partial class Mentor_Home : Form
    {
        private DomainModel.Mentor mentor;
        public Mentor_Home(DomainModel.Mentor mentor)
        {
            InitializeComponent();
            this.mentor = mentor;
        }

        

        private void Mentor_Home_Load(object sender, EventArgs e)
        {
            bunifuLabel1.Text = "Welcome " + mentor.FullName;
        }

        


        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mentor_ApproveMember mentor_ApproveMember = new Mentor_ApproveMember(this);
            mentor_ApproveMember.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mentor_EventApproval mentor_EventApproval = new Mentor_EventApproval(this, mentor);
            mentor_EventApproval.Show();

        }
        

        

        

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            MentorLogin mentorLogin = new MentorLogin();
            mentorLogin.Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mentor_DeleteMember mentor_DeleteMember = new Mentor_DeleteMember(this, mentor);
            mentor_DeleteMember.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
