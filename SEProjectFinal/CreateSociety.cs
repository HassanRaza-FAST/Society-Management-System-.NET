﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;
using System.Configuration;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SEProjectFinal.DomainModel;

namespace SEProjectFinal
{
    public partial class CreateSociety : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString;
        
        private SocietyMember_Home societyMemberHome;
        private Student_Home studentHome;

        public CreateSociety(SocietyMember_Home societyMemberHome)
        {
            InitializeComponent();
            this.Text = "Create Society";
            this.societyMemberHome = societyMemberHome;
            
        }

        public CreateSociety(Student_Home studentHome)
        {
            InitializeComponent();
            this.Text = "Create Society";
            this.studentHome = studentHome;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check if all text fields are filled in our form
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || comboBox1.SelectedIndex == -1 || string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            SocietyService societyService = new SocietyService(our_connection_string);
            SocietyApplication application = new SocietyApplication
            {
                StudentID = int.Parse(textBox1.Text.Trim()),
                SocietyName = textBox2.Text.Trim(),
                Description = richTextBox1.Text.Trim(),
                DepartmentName = comboBox1.SelectedItem.ToString()
            };
            int applicationId = societyService.CreateSocietyApplication(application);

            if (applicationId > 0)
            {
                MessageBox.Show("Society creation application submitted successfully.");
                textBox1.Text = "";
                textBox2.Text = "";
                richTextBox1.Text = "";
            }
            else
            {
                MessageBox.Show("An error occurred while submitting the society creation application.");
            }
        }


        private void returnbtn_Click(object sender, EventArgs e)
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
