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
        public Mentor_Home()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Mentor_ApproveMember mentor_ApproveMember = new Mentor_ApproveMember(this);
            mentor_ApproveMember.Show();
        }
    }
}
