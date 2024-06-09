using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace i211130
{
    public partial class Enrollstudent : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int studentId; // Variable to store the student ID
        public Enrollstudent()
        {
        }
            public Enrollstudent(int studentid)
            {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentId = studentid;
            }

        private void button2_Click(object sender, EventArgs e)
        {
            //back
            Studenthome cld = new Studenthome(studentId);
            cld.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //text box
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the class code entered by the student
                string classCode = textBox2.Text.Trim();

                // Check if the class code is empty
                if (string.IsNullOrEmpty(classCode))
                {
                    MessageBox.Show("Please enter a class code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the class code exists in the database
                cn.Open();
                cm = new SqlCommand("SELECT ClassID FROM Class WHERE ClassroomCode = @ClassCode", cn);
                cm.Parameters.AddWithValue("@ClassCode", classCode);
                int classId = Convert.ToInt32(cm.ExecuteScalar());

                // Check if the class code is valid
                if (classId == 0)
                {
                    MessageBox.Show("Invalid class code. Please enter a valid class code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the student is already enrolled in the class
                cm = new SqlCommand("SELECT COUNT(*) FROM Enrollment WHERE StudentID = @StudentID AND ClassID = @ClassID", cn);
                cm.Parameters.AddWithValue("@StudentID", studentId);
                cm.Parameters.AddWithValue("@ClassID", classId);
                int enrollmentCount = Convert.ToInt32(cm.ExecuteScalar());

                // Check if the student is already enrolled
                if (enrollmentCount > 0)
                {
                    MessageBox.Show("You are already enrolled in this class.", "Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Enroll the student in the class
                cm = new SqlCommand("INSERT INTO Enrollment (StudentID, ClassID) VALUES (@StudentID, @ClassID)", cn);
                cm.Parameters.AddWithValue("@StudentID", studentId);
                cm.Parameters.AddWithValue("@ClassID", classId);
                cm.ExecuteNonQuery();

                MessageBox.Show("You have successfully enrolled in the class.", "Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            //student home page 
            Studenthome cld = new Studenthome(studentId);
            cld.Show();
            this.Hide();
        }
        private void Enrollstudent_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the class code entered by the student
                string classCode = textBox2.Text.Trim();

                // Check if the class code is empty
                if (string.IsNullOrEmpty(classCode))
                {
                    MessageBox.Show("Please enter a class code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the class code exists in the database
                cn.Open();
                cm = new SqlCommand("SELECT ClassID FROM Class WHERE ClassroomCode = @ClassCode", cn);
                cm.Parameters.AddWithValue("@ClassCode", classCode);
                int classId = Convert.ToInt32(cm.ExecuteScalar());

                // Check if the class code is valid
                if (classId == 0)
                {
                    MessageBox.Show("Invalid class code. Please enter a valid class code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the student is already enrolled in the class
                cm = new SqlCommand("SELECT COUNT(*) FROM Enrollment WHERE StudentID = @StudentID AND ClassID = @ClassID", cn);
                cm.Parameters.AddWithValue("@StudentID", studentId);
                cm.Parameters.AddWithValue("@ClassID", classId);
                int enrollmentCount = Convert.ToInt32(cm.ExecuteScalar());

                // Check if the student is already enrolled
                if (enrollmentCount > 0)
                {
                    MessageBox.Show("You are already enrolled in this class.", "Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Enroll the student in the class
                cm = new SqlCommand("INSERT INTO Enrollment (StudentID, ClassID) VALUES (@StudentID, @ClassID)", cn);
                cm.Parameters.AddWithValue("@StudentID", studentId);
                cm.Parameters.AddWithValue("@ClassID", classId);
                cm.ExecuteNonQuery();

                MessageBox.Show("You have successfully enrolled in the class.", "Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            //student home page 
            Studenthome cld = new Studenthome(studentId);
            cld.Show();
            this.Hide();
        }
    }
}