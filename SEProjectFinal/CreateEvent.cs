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
    public partial class CreateEvent : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;

        private SocietyExecutive_Home societyExecutive_Home;
        public CreateEvent(SocietyExecutive_Home societyExecutive_Home)
        {
            this.societyExecutive_Home = societyExecutive_Home;
            this.Text = "Create Event";
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            societyExecutive_Home.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check if all fields are filed in our form
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(richTextBox1.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }

            // Assuming you have text boxes for each of the fields
            string eventName = textBox3.Text;
            string description = richTextBox1.Text;
            string location = textBox4.Text;
            int createdByStudentID = int.Parse(textBox1.Text); // Assuming you have a TextBox for the student ID
            int societyID = int.Parse(textBox2.Text); // Assuming you have a TextBox for the society ID
            DateTime selectedDate = dateTimePicker1.Value;
            // Assuming you have a SocietyService instance



            SocietyService societyService = new SocietyService(our_connection_string);

            Event newEvent = new Event
            {
                SocietyID = societyID,
                EventName = eventName,
                Description = description,
                EventDate = selectedDate,
                Location = location,
                CreatedByStudentID = createdByStudentID
            };

            int eventId = societyService.CreateEvent(newEvent);
            if (eventId > 0)
            {
                MessageBox.Show("Event created successfully.");
            }
            else
            {
                MessageBox.Show("An error occurred while creating the event.");
            }
        }

        private void CreateEvent_Load(object sender, EventArgs e)
        {

        }
    }
}
