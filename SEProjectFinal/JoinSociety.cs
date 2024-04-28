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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SEProjectFinal
{
    public partial class JoinSociety : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        private Student_Home studentHome;
        private SocietyMember_Home societyMemberHome;

        public JoinSociety(Student_Home studentHome)
        {
            InitializeComponent();
            this.studentHome = studentHome;
            this.Text = "Join Society";
        }
        public JoinSociety(SocietyMember_Home societyMemberHome)
        {
            InitializeComponent();
            this.societyMemberHome = societyMemberHome;
            this.Text = "Join Society";

        }

        private void JoinSociety_Load(object sender, EventArgs e)
        {
            
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void joinbtn_Click(object sender, EventArgs e)
        {
            

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            //check if all fields and combobox are seletec  in our form otherwise show a message
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(richTextBox1.Text) || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            SocietyService societyService = new SocietyService(our_connection_string);
            MembershipRequest membershipRequest = new MembershipRequest
            {
                StudentID = int.Parse(textBox1.Text.Trim()),
                SocietyID = int.Parse(textBox2.Text.Trim()),
                DepartmentName = comboBox2.SelectedItem.ToString(),
                TeamName = comboBox1.SelectedItem.ToString(),
                Description = richTextBox1.Text.Trim()
            };
            int applicationId = societyService.CreateMembershipRequest(membershipRequest);

            if (applicationId > 0)
            {
                MessageBox.Show("Application for joining society submitted.");
                textBox1.Text = "";
                textBox2.Text = "";
                richTextBox1.Text = "";
            }
            else
            {
                MessageBox.Show("An error occurred while submitting the society membership application.");
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (societyMemberHome != null)
            {
                societyMemberHome.Show();
            }
            else
            {
                studentHome.Show();
            }
        }
    }
}
