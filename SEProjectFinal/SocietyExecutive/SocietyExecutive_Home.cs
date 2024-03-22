using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public SocietyExecutive_Home()
        {
            InitializeComponent();
            this.Text = "Society Executive Home";
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
    }
}
