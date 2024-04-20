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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Mentor_ApproveMember mentor_ApproveMember = new Mentor_ApproveMember(this);
            mentor_ApproveMember.Show();
        }

        private void Mentor_Home_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Mentor_EventApproval mentor_EventApproval = new Mentor_EventApproval(this, mentor);
            mentor_EventApproval.Show();
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
        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MentorLogin mentorLogin = new MentorLogin();
            mentorLogin.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Mentor_DeleteMember mentor_DeleteMember = new Mentor_DeleteMember(this, mentor);
            mentor_DeleteMember.Show();

        }

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
