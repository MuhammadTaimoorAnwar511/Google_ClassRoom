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

namespace i211130
{
    public partial class Studenthome : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int studentId; // Variable to store the student ID

        public Studenthome()
        {
            InitializeComponent();
        }

        public Studenthome(int studentId)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentId = studentId;
            LoadClassDetails();
        }

        private void LoadClassDetails()
        {
            try
            {
                ///////////////
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Userr WHERE UserID = @studentID", cn);
                cm.Parameters.AddWithValue("@studentID", studentId);
                dr = cm.ExecuteReader();
                dr.Read();
                String userids = dr["UserID"].ToString();
                String username = dr["Name"].ToString();
                label1.Text = " ID : " + userids;
                label2.Text = " NAME: " + username;
                dr.Close();
                cn.Close();
                /////////////////

                cn.Open();
                cm = new SqlCommand("SELECT c.ClassID, c.ClassName, c.ClassroomCode, u.Name AS TeacherName FROM Class c INNER JOIN Enrollment e ON c.ClassID = e.ClassID INNER JOIN Userr u ON c.TeacherID = u.UserID WHERE e.StudentID = @StudentID", cn);
                cm.Parameters.AddWithValue("@StudentID", studentId);
                dr = cm.ExecuteReader();

                int x = 10; // Initial x-coordinate for positioning controls
                int y = 10; // Initial y-coordinate for positioning controls
                int panelCount = 0; // Counter for the number of panels in the current row

                while (dr.Read())
                {
                    int classID = Convert.ToInt32(dr["ClassID"]);
                    string className = dr["ClassName"].ToString();
                    string classroomCode = dr["ClassroomCode"].ToString();
                    string teacherName = dr["TeacherName"].ToString();

                    // Create a new panel for each class
                    Panel classPanel = new Panel();
                    classPanel.BorderStyle = BorderStyle.FixedSingle;
                    classPanel.Size = new Size(400, 120);
                    classPanel.Location = new Point(x, y);
                    classPanel.BackColor = Color.LightGreen;
                    classPanel.Padding = new Padding(5); // Optional: Add padding to the panel

                    // Create and configure label for class ID
                    Label lblClassID = new Label();
                    lblClassID.Text = "Class ID: " + classID;
                    lblClassID.Location = new Point(10, 10);
                    lblClassID.AutoSize = true;
                    lblClassID.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblClassID);

                    // Create and configure label for class name
                    Label lblClassName = new Label();
                    lblClassName.Text = "Class Name: " + className;
                    lblClassName.Location = new Point(10, 30);
                    lblClassName.AutoSize = true;
                    lblClassName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblClassName);

                    // Create and configure label for classroom code
                    Label lblClassroomCode = new Label();
                    lblClassroomCode.Text = "Classroom Code: " + classroomCode;
                    lblClassroomCode.Location = new Point(10, 50);
                    lblClassroomCode.AutoSize = true;
                    lblClassroomCode.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblClassroomCode);

                    // Create and configure label for teacher name
                    Label lblTeacherName = new Label();
                    lblTeacherName.Text = "Teacher Name: " + teacherName;
                    lblTeacherName.Location = new Point(10, 70);
                    lblTeacherName.AutoSize = true;
                    lblTeacherName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblTeacherName);

                    // Create and configure detail button
                    Button btnDetails = new Button();
                    btnDetails.Text = "Details";
                    btnDetails.Location = new Point(10, 90);
                    btnDetails.Tag = classID; // Set the class ID as the button's tag for identifying the selected class
                    btnDetails.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btnDetails.ForeColor = Color.Black;
                    btnDetails.Click += btnDetails_Click; // Assign the click event handler
                    classPanel.Controls.Add(btnDetails);

                    // Add the class panel to the main panel
                    panel2.Controls.Add(classPanel);

                    // Increment the panelCount
                    panelCount++;

                    // If three panels are already in the row, move to the next row
                    if (panelCount == 2)
                    {
                        x = 10; // Reset x-coordinate to the starting position
                        y += 150; // Increment y-coordinate to move to the next row
                        panelCount = 0; // Reset the panelCount
                    }
                    else
                    {
                        x += 410; // Increment x-coordinate to move to the next column
                    }
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login cld = new Login();
            cld.Show();
            this.Hide();
        }
        private void btnDetails_Click(object sender, EventArgs e)
        {
            //detail button 
            Button btn = (Button)sender;
            int classID = (int)btn.Tag;
            Studentclassdetail cld = new Studentclassdetail(studentId, classID);
            cld.Show();
            this.Hide();
            // Create a dummy function to show the class details

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //class detail page 

        }

        private void Addclass_Click(object sender, EventArgs e)
        {
            //enrollment page
            Enrollstudent cld = new Enrollstudent(studentId);
            cld.Show();
            this.Hide();
        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void Studenthome_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //id
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //NAME
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}