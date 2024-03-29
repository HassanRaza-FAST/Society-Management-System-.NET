using Microsoft.Data.SqlClient;
using SEProjectFinal.BLL;
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
    public partial class StudentLogin : Form
    {
        string our_connection_string = ConfigurationManager.ConnectionStrings["our_database"].ConnectionString; 
        public StudentLogin()
        {
            InitializeComponent();
            this.Text = "Student Login";
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Student student = new Student();

        //    using (SqlConnection connection = new SqlConnection(our_connection_string))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand())
        //        {
        //            command.Connection = connection;

        //            // Check student credentials
        //            command.CommandText = "SELECT * FROM Students WHERE Email = @Email AND Password = @Password";
        //            command.Parameters.AddWithValue("@Email", textBox1.Text.Trim());
        //            command.Parameters.AddWithValue("@Password", textBox2.Text.Trim());

        //            try
        //            {
        //                int societyMemberId;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        reader.Read();
        //                        student.StudentID = reader.GetInt32(0);
        //                        student.Username = reader.GetString(1);
        //                        student.Password = reader.GetString(2);
        //                        student.Email = reader.GetString(3);
        //                        student.FullName = reader.GetString(4);

        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Invalid email or password");
        //                        return;
        //                    }
        //                }



        //                // Check if student is a society member
        //                command.CommandText = "SELECT SocietyMemberID FROM SocietyMembers WHERE StudentID = @studentId";
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@studentId", student.StudentID);

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        reader.Read();
        //                        societyMemberId = reader.GetInt32(0); // Get the SocietyMemberID
        //                    }
        //                    else
        //                    {
        //                        // Student is not a society member, just a student
        //                        // Show the appropriate screen
        //                        this.Hide();
        //                        Student_Home student_Home = new Student_Home();

        //                        student_Home.Show();
        //                        return;
        //                    }
        //                }

        //                // Check if society member is an executive
        //                command.CommandText = "SELECT * FROM SocietyExecutives WHERE SocietyMemberID = @societyMemberId";
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@societyMemberId", societyMemberId);

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        // Student is an executive member
        //                        // Show the appropriate screen
        //                        this.Hide();
        //                        SocietyExecutive_Home societyExecutive_Home = new SocietyExecutive_Home();
        //                        societyExecutive_Home.Show();
        //                    }
        //                    else
        //                    {
        //                        // Student is not an executive member, just a society member
        //                        // Show the appropriate screen
        //                        this.Hide();
        //                        SocietyMember_Home societyMember_Home = new SocietyMember_Home(student);
        //                        societyMember_Home.Show();
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any errors that might have occurred
        //                MessageBox.Show("An error occurred: " + ex.Message);
        //            }
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            StudentService studentService = new StudentService(our_connection_string);
            Student student = studentService.GetStudentByEmailAndPassword(textBox1.Text.Trim(), textBox2.Text.Trim());

            if (student == null)
            {
                MessageBox.Show("Invalid email or password");
                return;
            }

            int? societyMemberId = studentService.GetSocietyMemberIdByStudentId(student.StudentID);

            if (societyMemberId == null)
            {
                // Student is not a society member, just a student
                // Show the appropriate screen
                this.Hide();
                Student_Home student_Home = new Student_Home(student);
                student_Home.Show();
                return;
            }

            bool isSocietyExecutive = studentService.IsSocietyExecutive(societyMemberId.Value);

            if (isSocietyExecutive)
            {
                // Student is an executive member
                // Show the appropriate screen
                this.Hide();
                SocietyExecutive_Home societyExecutive_Home = new SocietyExecutive_Home(student);
                societyExecutive_Home.Show();
            }
            else
            {
                // Student is not an executive member, just a society member
                // Show the appropriate screen
                this.Hide();
                SocietyMember_Home societyMember_Home = new SocietyMember_Home(student);
                societyMember_Home.Show();
            }
        }



        private void returnbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }

        private void StudentLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
