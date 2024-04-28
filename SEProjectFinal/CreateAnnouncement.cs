using SEProjectFinal.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SEProjectFinal
{
    public partial class CreateAnnouncement : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        SocietyExecutive_Home societyExecutive_Home;
        public CreateAnnouncement(SocietyExecutive_Home societyExecutive_Home)
        {
            InitializeComponent();
            this.societyExecutive_Home = societyExecutive_Home;
        }
        
        private void CreateAnnouncement_Load(object sender, EventArgs e)
        {
            
        }

        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            societyExecutive_Home.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            //check if all fields are filed in our form
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            // Assuming you have text boxes for each of the fields
            string title = textBox3.Text;
            string description = richTextBox1.Text;
            int createdByStudentID = int.Parse(textBox1.Text);
            int societyID = int.Parse(textBox2.Text);




            SocietyService societyService = new SocietyService(our_connection_string);

            Announcement newAnnouncement = new Announcement
            {
                SocietyID = societyID,
                Description = description,
                Title = title,
                CreatedByStudentID = createdByStudentID
            };

            int AnnouncementId = societyService.CreateAnnouncement(newAnnouncement);
            if (AnnouncementId > 0)
            {
                MessageBox.Show("Announcement created successfully.");
            }
            else
            {
                MessageBox.Show("An error occurred while creating the Announcement.");
            }
        }
    }
}
